namespace Setup
{
    partial class mainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.cbxPhuongXa = new System.Windows.Forms.ComboBox();
            this.lbPhuongXa = new System.Windows.Forms.Label();
            this.btFilter = new System.Windows.Forms.Button();
            this.pnFilter = new System.Windows.Forms.Panel();
            this.pnGridView = new System.Windows.Forms.Panel();
            this.dgvFilter = new System.Windows.Forms.DataGridView();
            this.pnResult = new System.Windows.Forms.Panel();
            this.lbResult = new System.Windows.Forms.Label();
            this.txtGF1 = new System.Windows.Forms.TextBox();
            this.txtGF2 = new System.Windows.Forms.TextBox();
            this.txtGF3 = new System.Windows.Forms.TextBox();
            this.txtGF4 = new System.Windows.Forms.TextBox();
            this.btCRF = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbSugestion = new System.Windows.Forms.Label();
            this.chartNew = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnFilter.SuspendLayout();
            this.pnGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).BeginInit();
            this.pnResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartNew)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxPhuongXa
            // 
            this.cbxPhuongXa.FormattingEnabled = true;
            this.cbxPhuongXa.Location = new System.Drawing.Point(98, 28);
            this.cbxPhuongXa.Name = "cbxPhuongXa";
            this.cbxPhuongXa.Size = new System.Drawing.Size(121, 21);
            this.cbxPhuongXa.TabIndex = 0;
            // 
            // lbPhuongXa
            // 
            this.lbPhuongXa.AutoSize = true;
            this.lbPhuongXa.Location = new System.Drawing.Point(35, 31);
            this.lbPhuongXa.Name = "lbPhuongXa";
            this.lbPhuongXa.Size = new System.Drawing.Size(57, 13);
            this.lbPhuongXa.TabIndex = 1;
            this.lbPhuongXa.Text = "PhuongXa";
            // 
            // btFilter
            // 
            this.btFilter.Location = new System.Drawing.Point(255, 26);
            this.btFilter.Name = "btFilter";
            this.btFilter.Size = new System.Drawing.Size(75, 23);
            this.btFilter.TabIndex = 2;
            this.btFilter.Text = "Filter";
            this.btFilter.UseVisualStyleBackColor = true;
            this.btFilter.Click += new System.EventHandler(this.btFilter_Click);
            // 
            // pnFilter
            // 
            this.pnFilter.Controls.Add(this.btFilter);
            this.pnFilter.Controls.Add(this.cbxPhuongXa);
            this.pnFilter.Controls.Add(this.lbPhuongXa);
            this.pnFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnFilter.Location = new System.Drawing.Point(0, 0);
            this.pnFilter.Name = "pnFilter";
            this.pnFilter.Size = new System.Drawing.Size(817, 73);
            this.pnFilter.TabIndex = 3;
            // 
            // pnGridView
            // 
            this.pnGridView.Controls.Add(this.chartNew);
            this.pnGridView.Controls.Add(this.dgvFilter);
            this.pnGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnGridView.Location = new System.Drawing.Point(0, 73);
            this.pnGridView.Name = "pnGridView";
            this.pnGridView.Size = new System.Drawing.Size(817, 148);
            this.pnGridView.TabIndex = 4;
            // 
            // dgvFilter
            // 
            this.dgvFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilter.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvFilter.Location = new System.Drawing.Point(0, 0);
            this.dgvFilter.Name = "dgvFilter";
            this.dgvFilter.Size = new System.Drawing.Size(508, 148);
            this.dgvFilter.TabIndex = 0;
            // 
            // pnResult
            // 
            this.pnResult.Controls.Add(this.lbSugestion);
            this.pnResult.Controls.Add(this.label5);
            this.pnResult.Controls.Add(this.label4);
            this.pnResult.Controls.Add(this.label3);
            this.pnResult.Controls.Add(this.label2);
            this.pnResult.Controls.Add(this.label1);
            this.pnResult.Controls.Add(this.btCRF);
            this.pnResult.Controls.Add(this.txtGF4);
            this.pnResult.Controls.Add(this.txtGF3);
            this.pnResult.Controls.Add(this.txtGF2);
            this.pnResult.Controls.Add(this.txtGF1);
            this.pnResult.Controls.Add(this.lbResult);
            this.pnResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnResult.Location = new System.Drawing.Point(0, 221);
            this.pnResult.Name = "pnResult";
            this.pnResult.Size = new System.Drawing.Size(817, 262);
            this.pnResult.TabIndex = 5;
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.Location = new System.Drawing.Point(109, 76);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(37, 13);
            this.lbResult.TabIndex = 0;
            this.lbResult.Text = "Predic";
            // 
            // txtGF1
            // 
            this.txtGF1.Location = new System.Drawing.Point(86, 6);
            this.txtGF1.Name = "txtGF1";
            this.txtGF1.Size = new System.Drawing.Size(100, 20);
            this.txtGF1.TabIndex = 1;
            // 
            // txtGF2
            // 
            this.txtGF2.Location = new System.Drawing.Point(192, 6);
            this.txtGF2.Name = "txtGF2";
            this.txtGF2.Size = new System.Drawing.Size(100, 20);
            this.txtGF2.TabIndex = 2;
            // 
            // txtGF3
            // 
            this.txtGF3.Location = new System.Drawing.Point(298, 6);
            this.txtGF3.Name = "txtGF3";
            this.txtGF3.Size = new System.Drawing.Size(100, 20);
            this.txtGF3.TabIndex = 3;
            // 
            // txtGF4
            // 
            this.txtGF4.Location = new System.Drawing.Point(404, 6);
            this.txtGF4.Name = "txtGF4";
            this.txtGF4.Size = new System.Drawing.Size(100, 20);
            this.txtGF4.TabIndex = 4;
            // 
            // btCRF
            // 
            this.btCRF.Location = new System.Drawing.Point(16, 66);
            this.btCRF.Name = "btCRF";
            this.btCRF.Size = new System.Drawing.Size(75, 23);
            this.btCRF.TabIndex = 5;
            this.btCRF.Text = "Predic";
            this.btCRF.UseVisualStyleBackColor = true;
            this.btCRF.Click += new System.EventHandler(this.btCRF_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Informations:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hoc Van %";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Co viec lam %";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(295, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ko co TATS %";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(401, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Goc Ngoai TInh %";
            // 
            // lbSugestion
            // 
            this.lbSugestion.AutoSize = true;
            this.lbSugestion.Location = new System.Drawing.Point(442, 76);
            this.lbSugestion.Name = "lbSugestion";
            this.lbSugestion.Size = new System.Drawing.Size(66, 13);
            this.lbSugestion.TabIndex = 11;
            this.lbSugestion.Text = "Suggestion: ";
            // 
            // chartNew
            // 
            chartArea1.Name = "ChartArea1";
            this.chartNew.ChartAreas.Add(chartArea1);
            this.chartNew.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartNew.Legends.Add(legend1);
            this.chartNew.Location = new System.Drawing.Point(508, 0);
            this.chartNew.Name = "chartNew";
            this.chartNew.Size = new System.Drawing.Size(309, 148);
            this.chartNew.TabIndex = 12;
            this.chartNew.Text = "CHART";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 483);
            this.Controls.Add(this.pnResult);
            this.Controls.Add(this.pnGridView);
            this.Controls.Add(this.pnFilter);
            this.Name = "mainForm";
            this.Text = "Form1";
            this.pnFilter.ResumeLayout(false);
            this.pnFilter.PerformLayout();
            this.pnGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilter)).EndInit();
            this.pnResult.ResumeLayout(false);
            this.pnResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartNew)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxPhuongXa;
        private System.Windows.Forms.Label lbPhuongXa;
        private System.Windows.Forms.Button btFilter;
        private System.Windows.Forms.Panel pnFilter;
        private System.Windows.Forms.Panel pnGridView;
        private System.Windows.Forms.DataGridView dgvFilter;
        private System.Windows.Forms.Panel pnResult;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.Button btCRF;
        private System.Windows.Forms.TextBox txtGF4;
        private System.Windows.Forms.TextBox txtGF3;
        private System.Windows.Forms.TextBox txtGF2;
        private System.Windows.Forms.TextBox txtGF1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSugestion;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartNew;
    }
}

