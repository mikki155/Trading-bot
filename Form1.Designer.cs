namespace BitmexBot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonBuy = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.nudQty = new System.Windows.Forms.NumericUpDown();
            this.OrderType = new System.Windows.Forms.ComboBox();
            this.candleTimer = new System.Windows.Forms.Timer(this.components);
            this.botButton = new System.Windows.Forms.Button();
            this.botTimer = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.botNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.stratBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.networks = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.breakoutPrice = new System.Windows.Forms.NumericUpDown();
            this.stopLoss = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.botNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.breakoutPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopLoss)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBuy
            // 
            this.buttonBuy.BackColor = System.Drawing.Color.Lime;
            this.buttonBuy.Location = new System.Drawing.Point(12, 100);
            this.buttonBuy.Name = "buttonBuy";
            this.buttonBuy.Size = new System.Drawing.Size(75, 23);
            this.buttonBuy.TabIndex = 0;
            this.buttonBuy.Text = "Buy";
            this.buttonBuy.UseVisualStyleBackColor = false;
            this.buttonBuy.Click += new System.EventHandler(this.buttonBuy_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(12, 143);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Sell";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.buttonSell_Click);
            // 
            // nudQty
            // 
            this.nudQty.Location = new System.Drawing.Point(102, 146);
            this.nudQty.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudQty.Name = "nudQty";
            this.nudQty.Size = new System.Drawing.Size(121, 20);
            this.nudQty.TabIndex = 2;
            this.nudQty.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // OrderType
            // 
            this.OrderType.FormattingEnabled = true;
            this.OrderType.Items.AddRange(new object[] {
            "Limit",
            "Market"});
            this.OrderType.Location = new System.Drawing.Point(102, 100);
            this.OrderType.Name = "OrderType";
            this.OrderType.Size = new System.Drawing.Size(121, 21);
            this.OrderType.TabIndex = 3;
            this.OrderType.SelectedIndexChanged += new System.EventHandler(this.OrderType_SelectedIndexChanged);
            // 
            // candleTimer
            // 
            this.candleTimer.Enabled = true;
            this.candleTimer.Interval = 10000;
            this.candleTimer.Tick += new System.EventHandler(this.candleTimer_Tick);
            // 
            // botButton
            // 
            this.botButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.botButton.Location = new System.Drawing.Point(532, 92);
            this.botButton.Name = "botButton";
            this.botButton.Size = new System.Drawing.Size(126, 74);
            this.botButton.TabIndex = 7;
            this.botButton.Text = "Start bot";
            this.botButton.UseVisualStyleBackColor = false;
            this.botButton.Click += new System.EventHandler(this.botButton_Click);
            // 
            // botTimer
            // 
            this.botTimer.Enabled = true;
            this.botTimer.Interval = 12000;
            this.botTimer.Tick += new System.EventHandler(this.botTimer_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(18, 183);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(786, 87);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // botNumUpDown
            // 
            this.botNumUpDown.Location = new System.Drawing.Point(380, 146);
            this.botNumUpDown.Name = "botNumUpDown";
            this.botNumUpDown.Size = new System.Drawing.Size(126, 20);
            this.botNumUpDown.TabIndex = 9;
            this.botNumUpDown.ValueChanged += new System.EventHandler(this.botNumUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 31);
            this.label2.TabIndex = 12;
            this.label2.Text = "Manual trading:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(473, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 31);
            this.label3.TabIndex = 13;
            this.label3.Text = "Automated trading:";
            // 
            // stratBox
            // 
            this.stratBox.FormattingEnabled = true;
            this.stratBox.Items.AddRange(new object[] {
            "Choppy",
            "Runny",
            "Philakone",
            "Crossing",
            "Breakout"});
            this.stratBox.Location = new System.Drawing.Point(380, 100);
            this.stratBox.Name = "stratBox";
            this.stratBox.Size = new System.Drawing.Size(126, 21);
            this.stratBox.TabIndex = 10;
            this.stratBox.SelectedIndexChanged += new System.EventHandler(this.stratBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(377, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Trading strategies:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(377, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Quantity:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(99, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Quantity:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(99, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Order type:";
            // 
            // networks
            // 
            this.networks.FormattingEnabled = true;
            this.networks.Items.AddRange(new object[] {
            "Testnet",
            "Realnet"});
            this.networks.Location = new System.Drawing.Point(239, 96);
            this.networks.Name = "networks";
            this.networks.Size = new System.Drawing.Size(121, 21);
            this.networks.TabIndex = 17;
            this.networks.SelectedIndexChanged += new System.EventHandler(this.networks_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(236, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Network:";
            // 
            // breakoutPrice
            // 
            this.breakoutPrice.Location = new System.Drawing.Point(684, 97);
            this.breakoutPrice.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.breakoutPrice.Name = "breakoutPrice";
            this.breakoutPrice.Size = new System.Drawing.Size(120, 20);
            this.breakoutPrice.TabIndex = 19;
            this.breakoutPrice.ValueChanged += new System.EventHandler(this.breakoutPrice_ValueChanged);
            // 
            // stopLoss
            // 
            this.stopLoss.Location = new System.Drawing.Point(684, 146);
            this.stopLoss.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.stopLoss.Name = "stopLoss";
            this.stopLoss.Size = new System.Drawing.Size(120, 20);
            this.stopLoss.TabIndex = 20;
            this.stopLoss.ValueChanged += new System.EventHandler(this.stopLoss_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(681, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Breakout price:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(681, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Stop-loss:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 282);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.stopLoss);
            this.Controls.Add(this.breakoutPrice);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.networks);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stratBox);
            this.Controls.Add(this.botNumUpDown);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.botButton);
            this.Controls.Add(this.OrderType);
            this.Controls.Add(this.nudQty);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonBuy);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "BitmexBot";
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.botNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.breakoutPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopLoss)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBuy;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown nudQty;
        private System.Windows.Forms.ComboBox OrderType;
        private System.Windows.Forms.Timer candleTimer;
        private System.Windows.Forms.Button botButton;
        private System.Windows.Forms.Timer botTimer;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.NumericUpDown botNumUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox stratBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox networks;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown breakoutPrice;
        private System.Windows.Forms.NumericUpDown stopLoss;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}

