namespace turing
{
    partial class SettingsTuringForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsTuringForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.alphabetTB = new System.Windows.Forms.TextBox();
            this.minNUD = new System.Windows.Forms.NumericUpDown();
            this.maxNUD = new System.Windows.Forms.NumericUpDown();
            this.defaultWordTB = new System.Windows.Forms.TextBox();
            this.SaveB = new System.Windows.Forms.Button();
            this.countStateNUD = new System.Windows.Forms.NumericUpDown();
            this.startPositionNUD = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.minNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countStateNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPositionNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Алфавит";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Минимальное значение ленты";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(217, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Максимальное значение ленты";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Дефолтное слово";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(205, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Количество состояний (без 0)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(215, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Начальное положение головки";
            // 
            // alphabetTB
            // 
            this.alphabetTB.Location = new System.Drawing.Point(104, 7);
            this.alphabetTB.Name = "alphabetTB";
            this.alphabetTB.Size = new System.Drawing.Size(216, 20);
            this.alphabetTB.TabIndex = 4;
            // 
            // minNUD
            // 
            this.minNUD.Location = new System.Drawing.Point(243, 37);
            this.minNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.minNUD.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.minNUD.Name = "minNUD";
            this.minNUD.Size = new System.Drawing.Size(77, 20);
            this.minNUD.TabIndex = 5;
            this.minNUD.ValueChanged += new System.EventHandler(this.minNUD_ValueChanged);
            // 
            // maxNUD
            // 
            this.maxNUD.Location = new System.Drawing.Point(243, 75);
            this.maxNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.maxNUD.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.maxNUD.Name = "maxNUD";
            this.maxNUD.Size = new System.Drawing.Size(77, 20);
            this.maxNUD.TabIndex = 6;
            this.maxNUD.ValueChanged += new System.EventHandler(this.maxNUD_ValueChanged);
            // 
            // defaultWordTB
            // 
            this.defaultWordTB.Location = new System.Drawing.Point(180, 108);
            this.defaultWordTB.Name = "defaultWordTB";
            this.defaultWordTB.Size = new System.Drawing.Size(140, 20);
            this.defaultWordTB.TabIndex = 7;
            // 
            // SaveB
            // 
            this.SaveB.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveB.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveB.Location = new System.Drawing.Point(104, 214);
            this.SaveB.Name = "SaveB";
            this.SaveB.Size = new System.Drawing.Size(119, 23);
            this.SaveB.TabIndex = 8;
            this.SaveB.Text = "Сохранить";
            this.SaveB.UseVisualStyleBackColor = true;
            // 
            // countStateNUD
            // 
            this.countStateNUD.Location = new System.Drawing.Point(240, 138);
            this.countStateNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countStateNUD.Name = "countStateNUD";
            this.countStateNUD.Size = new System.Drawing.Size(77, 20);
            this.countStateNUD.TabIndex = 10;
            this.countStateNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // startPositionNUD
            // 
            this.startPositionNUD.Location = new System.Drawing.Point(240, 177);
            this.startPositionNUD.Name = "startPositionNUD";
            this.startPositionNUD.Size = new System.Drawing.Size(77, 20);
            this.startPositionNUD.TabIndex = 12;
            this.startPositionNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SettingsTuringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.SaveB;
            this.ClientSize = new System.Drawing.Size(329, 249);
            this.Controls.Add(this.startPositionNUD);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.countStateNUD);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SaveB);
            this.Controls.Add(this.defaultWordTB);
            this.Controls.Add(this.maxNUD);
            this.Controls.Add(this.minNUD);
            this.Controls.Add(this.alphabetTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsTuringForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.minNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countStateNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPositionNUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox alphabetTB;
        private System.Windows.Forms.NumericUpDown minNUD;
        private System.Windows.Forms.NumericUpDown maxNUD;
        private System.Windows.Forms.TextBox defaultWordTB;
        private System.Windows.Forms.Button SaveB;
        private System.Windows.Forms.NumericUpDown countStateNUD;
        private System.Windows.Forms.NumericUpDown startPositionNUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}