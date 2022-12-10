namespace calculator
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bt7 = new System.Windows.Forms.Button();
            this.bt9 = new System.Windows.Forms.Button();
            this.btBracket1 = new System.Windows.Forms.Button();
            this.bt4 = new System.Windows.Forms.Button();
            this.bt5 = new System.Windows.Forms.Button();
            this.bt6 = new System.Windows.Forms.Button();
            this.btMul = new System.Windows.Forms.Button();
            this.bt1 = new System.Windows.Forms.Button();
            this.bt2 = new System.Windows.Forms.Button();
            this.bt3 = new System.Windows.Forms.Button();
            this.btDiv = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
            this.bt0 = new System.Windows.Forms.Button();
            this.btComma = new System.Windows.Forms.Button();
            this.btMod = new System.Windows.Forms.Button();
            this.bt8 = new System.Windows.Forms.Button();
            this.btBracket2 = new System.Windows.Forms.Button();
            this.btSub = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.btEqual = new System.Windows.Forms.Button();
            this.del = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.reset_list = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_power = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Beige;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("굴림", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox1.Location = new System.Drawing.Point(19, 107);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(524, 63);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox1.WordWrap = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // bt7
            // 
            this.bt7.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt7.Location = new System.Drawing.Point(20, 209);
            this.bt7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt7.Name = "bt7";
            this.bt7.Size = new System.Drawing.Size(79, 80);
            this.bt7.TabIndex = 1;
            this.bt7.Text = "7";
            this.bt7.UseVisualStyleBackColor = true;
            this.bt7.Click += new System.EventHandler(this.bt7_Click);
            // 
            // bt9
            // 
            this.bt9.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt9.Location = new System.Drawing.Point(190, 208);
            this.bt9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt9.Name = "bt9";
            this.bt9.Size = new System.Drawing.Size(81, 80);
            this.bt9.TabIndex = 3;
            this.bt9.Text = "9";
            this.bt9.UseVisualStyleBackColor = true;
            this.bt9.Click += new System.EventHandler(this.bt9_Click);
            // 
            // btBracket1
            // 
            this.btBracket1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btBracket1.Location = new System.Drawing.Point(397, 209);
            this.btBracket1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btBracket1.Name = "btBracket1";
            this.btBracket1.Size = new System.Drawing.Size(70, 80);
            this.btBracket1.TabIndex = 4;
            this.btBracket1.Text = "(";
            this.btBracket1.UseVisualStyleBackColor = true;
            this.btBracket1.Click += new System.EventHandler(this.btBracket1_Click);
            // 
            // bt4
            // 
            this.bt4.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt4.Location = new System.Drawing.Point(20, 295);
            this.bt4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt4.Name = "bt4";
            this.bt4.Size = new System.Drawing.Size(79, 80);
            this.bt4.TabIndex = 5;
            this.bt4.Text = "4";
            this.bt4.UseVisualStyleBackColor = true;
            this.bt4.Click += new System.EventHandler(this.bt4_Click);
            // 
            // bt5
            // 
            this.bt5.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt5.Location = new System.Drawing.Point(105, 294);
            this.bt5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt5.Name = "bt5";
            this.bt5.Size = new System.Drawing.Size(79, 80);
            this.bt5.TabIndex = 6;
            this.bt5.Text = "5";
            this.bt5.UseVisualStyleBackColor = true;
            this.bt5.Click += new System.EventHandler(this.bt5_Click);
            // 
            // bt6
            // 
            this.bt6.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt6.Location = new System.Drawing.Point(190, 294);
            this.bt6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt6.Name = "bt6";
            this.bt6.Size = new System.Drawing.Size(81, 80);
            this.bt6.TabIndex = 7;
            this.bt6.Text = "6";
            this.bt6.UseVisualStyleBackColor = true;
            this.bt6.Click += new System.EventHandler(this.bt6_Click);
            // 
            // btMul
            // 
            this.btMul.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btMul.Location = new System.Drawing.Point(397, 295);
            this.btMul.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btMul.Name = "btMul";
            this.btMul.Size = new System.Drawing.Size(70, 80);
            this.btMul.TabIndex = 8;
            this.btMul.Text = "*";
            this.btMul.UseVisualStyleBackColor = true;
            this.btMul.Click += new System.EventHandler(this.btMul_Click);
            // 
            // bt1
            // 
            this.bt1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt1.Location = new System.Drawing.Point(20, 383);
            this.bt1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt1.Name = "bt1";
            this.bt1.Size = new System.Drawing.Size(79, 80);
            this.bt1.TabIndex = 9;
            this.bt1.Text = "1";
            this.bt1.UseVisualStyleBackColor = true;
            this.bt1.Click += new System.EventHandler(this.bt1_Click);
            // 
            // bt2
            // 
            this.bt2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt2.Location = new System.Drawing.Point(105, 382);
            this.bt2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt2.Name = "bt2";
            this.bt2.Size = new System.Drawing.Size(79, 80);
            this.bt2.TabIndex = 10;
            this.bt2.Text = "2";
            this.bt2.UseVisualStyleBackColor = true;
            this.bt2.Click += new System.EventHandler(this.bt2_Click);
            // 
            // bt3
            // 
            this.bt3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt3.Location = new System.Drawing.Point(190, 382);
            this.bt3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt3.Name = "bt3";
            this.bt3.Size = new System.Drawing.Size(81, 80);
            this.bt3.TabIndex = 11;
            this.bt3.Text = "3";
            this.bt3.UseVisualStyleBackColor = true;
            this.bt3.Click += new System.EventHandler(this.bt3_Click);
            // 
            // btDiv
            // 
            this.btDiv.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btDiv.Location = new System.Drawing.Point(472, 295);
            this.btDiv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btDiv.Name = "btDiv";
            this.btDiv.Size = new System.Drawing.Size(70, 80);
            this.btDiv.TabIndex = 12;
            this.btDiv.Text = "/";
            this.btDiv.UseVisualStyleBackColor = true;
            this.btDiv.Click += new System.EventHandler(this.btDiv_Click);
            // 
            // btReset
            // 
            this.btReset.BackColor = System.Drawing.Color.Silver;
            this.btReset.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btReset.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btReset.Location = new System.Drawing.Point(277, 468);
            this.btReset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(80, 80);
            this.btReset.TabIndex = 13;
            this.btReset.Text = "C";
            this.btReset.UseVisualStyleBackColor = false;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // bt0
            // 
            this.bt0.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt0.Location = new System.Drawing.Point(105, 468);
            this.bt0.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt0.Name = "bt0";
            this.bt0.Size = new System.Drawing.Size(79, 80);
            this.bt0.TabIndex = 14;
            this.bt0.Text = "0";
            this.bt0.UseVisualStyleBackColor = true;
            this.bt0.Click += new System.EventHandler(this.bt0_Click);
            // 
            // btComma
            // 
            this.btComma.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btComma.Location = new System.Drawing.Point(190, 468);
            this.btComma.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btComma.Name = "btComma";
            this.btComma.Size = new System.Drawing.Size(81, 80);
            this.btComma.TabIndex = 15;
            this.btComma.Text = ".";
            this.btComma.UseVisualStyleBackColor = true;
            this.btComma.Click += new System.EventHandler(this.btComma_Click);
            // 
            // btMod
            // 
            this.btMod.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btMod.Location = new System.Drawing.Point(397, 469);
            this.btMod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btMod.Name = "btMod";
            this.btMod.Size = new System.Drawing.Size(70, 80);
            this.btMod.TabIndex = 16;
            this.btMod.Text = "MOD";
            this.btMod.UseVisualStyleBackColor = true;
            this.btMod.Click += new System.EventHandler(this.btMod_Click);
            // 
            // bt8
            // 
            this.bt8.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt8.Location = new System.Drawing.Point(105, 208);
            this.bt8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt8.Name = "bt8";
            this.bt8.Size = new System.Drawing.Size(79, 80);
            this.bt8.TabIndex = 2;
            this.bt8.Text = "8";
            this.bt8.UseVisualStyleBackColor = true;
            this.bt8.Click += new System.EventHandler(this.bt8_Click);
            // 
            // btBracket2
            // 
            this.btBracket2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btBracket2.Location = new System.Drawing.Point(472, 209);
            this.btBracket2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btBracket2.Name = "btBracket2";
            this.btBracket2.Size = new System.Drawing.Size(70, 80);
            this.btBracket2.TabIndex = 17;
            this.btBracket2.Text = ")";
            this.btBracket2.UseVisualStyleBackColor = true;
            this.btBracket2.Click += new System.EventHandler(this.btBracket2_Click);
            // 
            // btSub
            // 
            this.btSub.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btSub.Location = new System.Drawing.Point(472, 383);
            this.btSub.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btSub.Name = "btSub";
            this.btSub.Size = new System.Drawing.Size(70, 80);
            this.btSub.TabIndex = 18;
            this.btSub.Text = "-";
            this.btSub.UseVisualStyleBackColor = true;
            this.btSub.Click += new System.EventHandler(this.btSub_Click);
            // 
            // btAdd
            // 
            this.btAdd.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btAdd.Location = new System.Drawing.Point(397, 383);
            this.btAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(70, 80);
            this.btAdd.TabIndex = 19;
            this.btAdd.Text = "+";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btEqual
            // 
            this.btEqual.BackColor = System.Drawing.Color.RosyBrown;
            this.btEqual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btEqual.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btEqual.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btEqual.Location = new System.Drawing.Point(472, 469);
            this.btEqual.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btEqual.Name = "btEqual";
            this.btEqual.Size = new System.Drawing.Size(70, 80);
            this.btEqual.TabIndex = 20;
            this.btEqual.Text = "=";
            this.btEqual.UseVisualStyleBackColor = false;
            this.btEqual.Click += new System.EventHandler(this.btEqual_Click);
            // 
            // del
            // 
            this.del.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.del.Location = new System.Drawing.Point(20, 470);
            this.del.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.del.Name = "del";
            this.del.Size = new System.Drawing.Size(79, 79);
            this.del.TabIndex = 22;
            this.del.Text = "\r\nDEL\r\n←";
            this.del.UseVisualStyleBackColor = true;
            this.del.Click += new System.EventHandler(this.del_Click);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("휴먼편지체", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(19, 36);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(193, 64);
            this.listBox1.TabIndex = 23;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // reset_list
            // 
            this.reset_list.Location = new System.Drawing.Point(427, 60);
            this.reset_list.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.reset_list.Name = "reset_list";
            this.reset_list.Size = new System.Drawing.Size(61, 40);
            this.reset_list.TabIndex = 24;
            this.reset_list.Text = "계산 기록 초기화";
            this.reset_list.UseVisualStyleBackColor = true;
            this.reset_list.Click += new System.EventHandler(this.reset_list_Click);
            // 
            // listBox2
            // 
            this.listBox2.Font = new System.Drawing.Font("휴먼편지체", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 15;
            this.listBox2.Location = new System.Drawing.Point(228, 36);
            this.listBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(193, 64);
            this.listBox2.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "계산 기록 <Double Click>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "후위연산식";
            // 
            // bt_power
            // 
            this.bt_power.BackColor = System.Drawing.Color.White;
            this.bt_power.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_power.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.bt_power.ForeColor = System.Drawing.Color.Red;
            this.bt_power.Location = new System.Drawing.Point(472, 7);
            this.bt_power.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bt_power.Name = "bt_power";
            this.bt_power.Size = new System.Drawing.Size(70, 35);
            this.bt_power.TabIndex = 28;
            this.bt_power.TabStop = false;
            this.bt_power.Text = "Power";
            this.bt_power.UseVisualStyleBackColor = false;
            this.bt_power.Click += new System.EventHandler(this.bt_power_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(205, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "<음수로 시작하는 연산은 미구현>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(305, 521);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "ESC";
            // 
            // Form1
            // 
            this.AcceptButton = this.btEqual;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btReset;
            this.ClientSize = new System.Drawing.Size(561, 570);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bt_power);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.reset_list);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.del);
            this.Controls.Add(this.btEqual);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.btSub);
            this.Controls.Add(this.btBracket2);
            this.Controls.Add(this.btMod);
            this.Controls.Add(this.btComma);
            this.Controls.Add(this.bt0);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.btDiv);
            this.Controls.Add(this.bt3);
            this.Controls.Add(this.bt2);
            this.Controls.Add(this.bt1);
            this.Controls.Add(this.btMul);
            this.Controls.Add(this.bt6);
            this.Controls.Add(this.bt5);
            this.Controls.Add(this.bt4);
            this.Controls.Add(this.btBracket1);
            this.Controls.Add(this.bt9);
            this.Controls.Add(this.bt8);
            this.Controls.Add(this.bt7);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("휴먼편지체", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 13, 6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mini Calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bt7;
        private System.Windows.Forms.Button bt9;
        private System.Windows.Forms.Button btBracket1;
        private System.Windows.Forms.Button bt4;
        private System.Windows.Forms.Button bt5;
        private System.Windows.Forms.Button bt6;
        private System.Windows.Forms.Button btMul;
        private System.Windows.Forms.Button bt1;
        private System.Windows.Forms.Button bt2;
        private System.Windows.Forms.Button bt3;
        private System.Windows.Forms.Button btDiv;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.Button bt0;
        private System.Windows.Forms.Button btComma;
        private System.Windows.Forms.Button btMod;
        private System.Windows.Forms.Button bt8;
        private System.Windows.Forms.Button btBracket2;
        private System.Windows.Forms.Button btSub;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btEqual;
        private System.Windows.Forms.Button del;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button reset_list;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bt_power;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

