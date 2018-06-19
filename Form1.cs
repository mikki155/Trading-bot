using BitMEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitmexBot
{
    public partial class Form1 : Form
    {
        static string privatekeytest = //YOUR TESTNET KEY HERE;
        static string bitmexSecrettest = //YOUR TESTNET SECRET HERE;
        static string bitmexdomaintest = "https://testnet.bitmex.com";

        static string privatekeyreal = //YOUR KEY HERE;
        static string bitmexSecretreal = //YOUR SECRET HERE;
        static string bitmexdomainreal = "https://www.bitmex.com";

        private string Symbol = "XBTUSD";
        private int Depth = 1; //depth of the order book
        int count = 250; //number of candles
        double tickSize = 0.5; //price tick on Bitmex
        int desQty = 0;
        string timeframe = "5m";
        string strategy = "";
        int EMA1Period = 55;
        int EMA2Period = 200;
        int EMA3Period = 30;
        int bPrice = 0;
        int stoploss = 0;

        bool Running = false; //is the bot running?
        string Mode = "Wait"; //what is the bot doing?
        
        List<Position> OpenPos = new List<Position>();
        List<Order> OpenOrd = new List<Order>();
        List<OrderBook> CurrentBook = new List<OrderBook>();
        List<Candle> Candles = new List<Candle>();
        BitMEXApi bitmex;

        public Form1()
        {
            InitializeComponent();
            InitializeDropdown();
            InitializeAPI();
        }

        private void InitializeDropdown()
        {
            OrderType.SelectedIndex = 0;
            stratBox.SelectedIndex = 0;
            networks.SelectedIndex = 0;
        }

        private void networks_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        
        private void InitializeAPI()
        {
            if (networks.SelectedItem.ToString() == "Testnet")
            {

                bitmex = new BitMEXApi(privatekeytest, bitmexSecrettest, bitmexdomaintest);
            }
            else if (networks.SelectedItem.ToString() == "Realnet")
            {

                bitmex = new BitMEXApi(privatekeyreal, bitmexSecretreal, bitmexdomainreal);
            }
        }

        public void botNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            desQty = Convert.ToInt32(botNumUpDown.Value);
        }

        private void MakeMarketOrder(string Symbol, string Side, int Qty)
        {
            if (Side == "Buy")
            {
                var BuyOrder = bitmex.PostMarketOrders(Symbol, Side, Qty);
            }
            else if (Side == "Sell")
            {
                var SellOrder = bitmex.PostMarketOrders(Symbol, Side, Qty);
            }
        }

        private double CalcMakerOrder(string Symbol, string Side, double ticksize)
        {
            CurrentBook = bitmex.GetOrderBook(Symbol, Depth);

            double SellPrice = CurrentBook.Where(a => a.Side == "Sell").FirstOrDefault().Price; //uses linq
            double BuyPrice = CurrentBook.Where(a => a.Side == "Buy").FirstOrDefault().Price;

            double OrderPrice = 0;

            switch (Side)
            {
                case "Buy":
                    OrderPrice = BuyPrice;
                    if (SellPrice > BuyPrice + ticksize)
                    {
                        OrderPrice = OrderPrice + ticksize;
                    }
                    break;

                case "Sell":
                    OrderPrice = SellPrice;
                    if (SellPrice > BuyPrice + ticksize)
                    {
                        OrderPrice = OrderPrice - ticksize;
                    }
                    break;
            }
            return OrderPrice;
        }

        private void MakeLimitOrder(string Symbol, string Side, int Qty)
        {
            double Price = 0;
            if (Side == "Buy")
            {
                Price = CalcMakerOrder(Symbol, Side, tickSize);
                var BuyOrder = bitmex.PostLimitOrders(Symbol, Side, Price, Qty);
            }
            else if (Side == "Sell")
            {
                Price = CalcMakerOrder(Symbol, Side, tickSize);
                var SellOrder = bitmex.PostLimitOrders(Symbol, Side, Price, Qty);
            }

        }

        private void PlaceOrder(string Symbol, string Side, int Qty, string Type)
        {
            switch (Side)
            {
                case "Buy":
                    switch (Type)
                    {
                        case "Limit":
                            MakeLimitOrder(Symbol, Side, Qty);
                            break;
                        case "Market":
                            MakeMarketOrder(Symbol, Side, Qty);
                            break;
                    }
                    break;
                case "Sell":
                    switch (Type)
                    {
                        case "Limit":
                            MakeLimitOrder(Symbol, Side, Qty);
                            break;
                        case "Market":
                            MakeMarketOrder(Symbol, Side, Qty);
                            break;
                    }
                    break;
            }
        }

        private void candleMAupdate()
        {
            Candles = bitmex.CandleInfo(Symbol, count, timeframe);
            Candles = Candles.OrderBy(a => a.TimeStamp).ToList(); //order from oldest to newest


            foreach (Candle c in Candles)
            {
                c.PCC = Candles.Where(a => a.TimeStamp < c.TimeStamp).Count();
                
                if (c.PCC >= EMA1Period)
                {
                    double p1 = EMA1Period + 1;
                    double EMAMult1 = Convert.ToDouble(2 / p1);
                    if (c.PCC == EMA1Period)
                    {
                        c.EMA1 = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(EMA1Period).Average(a => a.Close);
                    }
                    else
                    {
                        double? LastEMA1 = Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().EMA1;
                        c.EMA1 = ((c.Close - LastEMA1)*EMAMult1) + LastEMA1;
                    }
                }
                if (c.PCC >= EMA2Period)
                {
                    double p2 = EMA2Period + 1;
                    double EMAMult2 = Convert.ToDouble(2 / p2);
                    if (c.PCC == EMA2Period)
                    {
                        c.EMA2 = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(EMA2Period).Average(a => a.Close);
                    }
                    else
                    {
                        double? LastEMA2 = Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().EMA2;
                        c.EMA2 = ((c.Close - LastEMA2) * EMAMult2) + LastEMA2;
                    }
                }
                if (c.PCC >= EMA3Period)
                {
                    double p3 = EMA3Period + 1;
                    double EMAMult3 = Convert.ToDouble(2 / p3);
                    if (c.PCC == EMA3Period)
                    {
                        c.EMA3 = Candles.Where(a => a.TimeStamp <= c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(EMA3Period).Average(a => a.Close);
                    }
                    else
                    {
                        double? LastEMA3 = Candles.Where(a => a.TimeStamp < c.TimeStamp).OrderByDescending(a => a.TimeStamp).Take(1).FirstOrDefault().EMA3;
                        c.EMA3 = ((c.Close - LastEMA3) * EMAMult3) + LastEMA3;
                    }
                }
            }
            if (Running)
            {
                SetBotMode();
                botButton.Text = "Bot mode: " + Mode;

            }
        }

        private void SetBotMode()
        {
            if (strategy == "Choppy")
            {
                if (Candles[count - 2].EMA1 > Candles[count - 2].EMA2)
                {
                    Mode = "Buy";
                }
                else if (Candles[count - 2].EMA1 < Candles[count - 2].EMA2)
                {
                    Mode = "Sell";
                }
                else
                {
                    Mode = "Wait";
                }
            }
            else if (strategy == "Runny")
            {
                if (Candles[count - 2].EMA3 > Candles[count - 2].EMA1)
                {
                    Mode = "Buy";
                }
                else if (Candles[count - 2].EMA3 < Candles[count - 2].EMA1)
                {
                    Mode = "Sell";
                }
                else
                {
                    Mode = "Wait";
                }
            }
            else if (strategy == "Philakone")
            {
                if (Candles[count-2].Close > 1.003*Candles[count - 2].EMA2)
                {
                    Mode = "Buy";
                }
                else if (Candles[count - 2].Close < 0.997*Candles[count - 2].EMA2)
                {
                    Mode = "Sell";
                }
                else
                {
                    Mode = "Wait";
                }
            }
            else if (strategy == "Crossing")
            {
                if (Candles[count - 2].Close > 1.005*Candles[count - 2].EMA1)
                {
                    Mode = "Buy";
                }
                else if (Candles[count - 2].Close < 0.995*Candles[count - 2].EMA1)
                {
                    Mode = "Sell";
                }
                else
                {
                    Mode = "Wait";
                }
            }
            else if (strategy == "Breakout")
            {
                if (bPrice > stoploss)
                {
                    if (Candles[count - 2].Close > bPrice)
                    {
                        Mode = "Buy";
                    }
                    else if (Candles[count - 2].Close < stoploss)
                    {
                        Mode = "Sell";
                    }
                    else
                    {
                        Mode = "Wait";
                    }
                }
                else if (stoploss > bPrice)
                {
                    if (Candles[count - 2].Close < bPrice)
                    {
                        Mode = "Sell";
                    }
                    else if (Candles[count - 2].Close > stoploss)
                    {
                        Mode = "Buy";
                    }
                    else
                    {
                        Mode = "Wait";
                    }
                }
                else
                {
                    Mode = "Wait";
                }
            }
        }

        private void buttonBuy_Click(object sender, EventArgs e)
        {
            bitmex.CancelAllOpenOrders(Symbol);
            PlaceOrder(Symbol, "Buy", Convert.ToInt32(nudQty.Value), OrderType.SelectedItem.ToString());
        }

        private void buttonSell_Click(object sender, EventArgs e)
        {
            bitmex.CancelAllOpenOrders(Symbol);
            PlaceOrder(Symbol, "Sell", Convert.ToInt32(nudQty.Value), OrderType.SelectedItem.ToString());
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void OrderType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void candleTimer_Tick(object sender, EventArgs e)
        {
            InitializeAPI();
            candleMAupdate();
        }

        private void botButton_Click(object sender, EventArgs e)
        {
            if (botButton.Text == "Start bot")
            {
                botButton.Text = "Stop";
                botButton.BackColor = Color.Red;
                Running = true;
            }
            else
            {
                botButton.Text = "Start bot";
                botButton.BackColor = Color.LightGreen;
                Running = false;
            }

        }

        private void botTimer_Tick(object sender, EventArgs e)
        {
            OpenPos = bitmex.GetOpenPositions(Symbol);
            OpenOrd = bitmex.GetOpenOrders(Symbol);
            int Qty = 0;
            string res = "";
            if (OpenPos.Any())
            {
                dataGridView1.DataSource = OpenPos;
            }
            if (OpenOrd.Any())
            {
                dataGridView1.DataSource = OpenOrd;
            }
            //implement case for when CurrentQty > desQty

            if (Running)
            {
                
                if (Mode == "Buy")
                {
                    if (OpenPos.Any())
                    {
                        if (Convert.ToInt32(OpenPos[0].CurrentQty) < desQty) //CurrentQty may be negative!
                        {
                            if (OpenOrd.Any(a => a.Side == "Sell"))
                            {
                                res = bitmex.CancelAllOpenOrders(Symbol);
                                Qty = desQty - Convert.ToInt32(OpenPos[0].CurrentQty);
                                MakeLimitOrder(Symbol, "Buy", Qty);
                            }
                            else if (OpenOrd.Any(a => a.Side == "Buy"))
                            {
                                res = bitmex.EditOrderPrice(OpenOrd[0].OrderId, CalcMakerOrder(Symbol, "Buy", tickSize));
                            }
                            else
                            {
                                Qty = desQty - Convert.ToInt32(OpenPos[0].CurrentQty);
                                MakeLimitOrder(Symbol, "Buy", Qty);
                            }

                        }
                        else if (Convert.ToInt32(OpenPos[0].CurrentQty) == desQty)
                        {
                            res = bitmex.CancelAllOpenOrders(Symbol);
                        }
                    }
                    else
                    {
                        if (strategy == "Breakout" && stoploss > bPrice && Candles[count - 2].Close > stoploss)
                        {
                            Mode = "Wait";
                        }
                        else
                        {
                            if (OpenOrd.Any())
                            {
                                if (OpenOrd.Any(a => a.Side == "Sell"))
                                {
                                    res = bitmex.CancelAllOpenOrders(Symbol);
                                    MakeLimitOrder(Symbol, "Buy", desQty);
                                }
                                else if (OpenOrd.Any(a => a.Side == "Buy"))
                                {
                                    res = bitmex.EditOrderPrice(OpenOrd[0].OrderId, CalcMakerOrder(Symbol, "Buy", tickSize));
                                }
                            }
                            else
                            {
                                MakeLimitOrder(Symbol, "Buy", desQty);
                            }
                        }
                    }

                }
                else if (Mode == "Sell")
                {
                    if (OpenPos.Any())
                    {
                        if (Math.Abs(Convert.ToInt32(OpenPos[0].CurrentQty)) < desQty) //CurrentQty may be negative!
                        {
                            if (OpenOrd.Any(a => a.Side == "Buy"))
                            {
                                res = bitmex.CancelAllOpenOrders(Symbol);
                                Qty = desQty - Math.Abs(Convert.ToInt32(OpenPos[0].CurrentQty));
                                MakeLimitOrder(Symbol, "Sell", Qty);
                            }
                            else if (OpenOrd.Any(a => a.Side == "Sell"))
                            {
                                res = bitmex.EditOrderPrice(OpenOrd[0].OrderId, CalcMakerOrder(Symbol, "Sell", tickSize));
                            }
                            else
                            {
                                Qty = desQty - Math.Abs(Convert.ToInt32(OpenPos[0].CurrentQty));
                                MakeLimitOrder(Symbol, "Sell", Qty);
                            }

                        }
                        else if (Convert.ToInt32(OpenPos[0].CurrentQty) == -desQty)
                        {
                            res = bitmex.CancelAllOpenOrders(Symbol);
                        }
                        else if (Convert.ToInt32(OpenPos[0].CurrentQty) >= 0)
                        {
                            Qty = desQty + Convert.ToInt32(OpenPos[0].CurrentQty);
                            if (OpenOrd.Any())
                            {
                                if (OpenOrd.Any(a => a.Side == "Buy"))
                                {
                                    res = bitmex.CancelAllOpenOrders(Symbol);
                                    MakeLimitOrder(Symbol, "Sell", Qty);
                                }
                                else if (OpenOrd.Any(a => a.Side == "Sell"))
                                {
                                    res = bitmex.EditOrderPrice(OpenOrd[0].OrderId, CalcMakerOrder(Symbol, "Sell", tickSize));
                                }
                            }
                            else
                            {
                                MakeLimitOrder(Symbol, "Sell", Qty);
                            }
                        }
                    }
                    else
                    {
                        if (strategy == "Breakout" && bPrice > stoploss && Candles[count - 2].Close < stoploss)
                        {
                            Mode = "Wait";
                        }
                        else
                        {
                            if (OpenOrd.Any())
                            {
                                if (OpenOrd.Any(a => a.Side == "Sell"))
                                {
                                    res = bitmex.EditOrderPrice(OpenOrd[0].OrderId, CalcMakerOrder(Symbol, "Sell", tickSize));
                                }
                                else if (OpenOrd.Any(a => a.Side == "Buy"))
                                {
                                    res = bitmex.CancelAllOpenOrders(Symbol);
                                    MakeLimitOrder(Symbol, "Sell", desQty);
                                }
                            }
                            else
                            {
                                MakeLimitOrder(Symbol, "Sell", desQty);
                            }
                        }
                    }
                }
                else if (Mode == "Wait")
                {
                    res = bitmex.CancelAllOpenOrders(Symbol);
                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void stratBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stratBox.SelectedItem.ToString() == "Choppy")
            {
                strategy = "Choppy";
                timeframe = "1h";
            }
            else if (stratBox.SelectedItem.ToString() == "Runny")
            {
                strategy = "Runny";
                timeframe = "1d";
            }
            else if (stratBox.SelectedItem.ToString() == "Philakone")
            {
                strategy = "Philakone";
                timeframe = "1h";
            }
            else if (stratBox.SelectedItem.ToString() == "Crossing")
            {
                strategy = "Crossing";
                timeframe = "1h";
            }
            else if (stratBox.SelectedItem.ToString() == "Breakout")
            {
                strategy = "Breakout";
                timeframe = "5m";
            }
        }

        private void breakoutPrice_ValueChanged(object sender, EventArgs e)
        {
            bPrice = Convert.ToInt32(breakoutPrice.Value);
        }

        private void stopLoss_ValueChanged(object sender, EventArgs e)
        {
            stoploss = Convert.ToInt32(stopLoss.Value);
        }
    }
    }
   
