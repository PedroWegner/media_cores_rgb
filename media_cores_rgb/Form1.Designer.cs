namespace media_cores_rgb
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureWeb = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.nomePessoa = new System.Windows.Forms.TextBox();
            this.salvarBtn = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.comboWeb = new System.Windows.Forms.ComboBox();
            this.tm = new System.Windows.Forms.Timer(this.components);
            this.flp = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWeb)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureWeb);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flp);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.nomePessoa);
            this.splitContainer1.Panel2.Controls.Add(this.salvarBtn);
            this.splitContainer1.Panel2.Controls.Add(this.startButton);
            this.splitContainer1.Panel2.Controls.Add(this.comboWeb);
            this.splitContainer1.Size = new System.Drawing.Size(884, 454);
            this.splitContainer1.SplitterDistance = 708;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureWeb
            // 
            this.pictureWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureWeb.Location = new System.Drawing.Point(0, 0);
            this.pictureWeb.Margin = new System.Windows.Forms.Padding(2);
            this.pictureWeb.Name = "pictureWeb";
            this.pictureWeb.Size = new System.Drawing.Size(708, 454);
            this.pictureWeb.TabIndex = 0;
            this.pictureWeb.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 26;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 422);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 15);
            this.label8.TabIndex = 19;
            this.label8.Text = "label8";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // nomePessoa
            // 
            this.nomePessoa.Location = new System.Drawing.Point(8, 366);
            this.nomePessoa.Margin = new System.Windows.Forms.Padding(2);
            this.nomePessoa.Name = "nomePessoa";
            this.nomePessoa.Size = new System.Drawing.Size(154, 20);
            this.nomePessoa.TabIndex = 11;
            this.nomePessoa.TextChanged += new System.EventHandler(this.nomePessoa_TextChanged);
            // 
            // salvarBtn
            // 
            this.salvarBtn.Location = new System.Drawing.Point(5, 389);
            this.salvarBtn.Margin = new System.Windows.Forms.Padding(2);
            this.salvarBtn.Name = "salvarBtn";
            this.salvarBtn.Size = new System.Drawing.Size(157, 19);
            this.salvarBtn.TabIndex = 5;
            this.salvarBtn.Text = "Salvar";
            this.salvarBtn.UseVisualStyleBackColor = true;
            this.salvarBtn.Click += new System.EventHandler(this.salvarBtn_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(2, 34);
            this.startButton.Margin = new System.Windows.Forms.Padding(2);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(160, 19);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // comboWeb
            // 
            this.comboWeb.FormattingEnabled = true;
            this.comboWeb.Location = new System.Drawing.Point(2, 10);
            this.comboWeb.Margin = new System.Windows.Forms.Padding(2);
            this.comboWeb.Name = "comboWeb";
            this.comboWeb.Size = new System.Drawing.Size(161, 21);
            this.comboWeb.TabIndex = 0;
            // 
            // tm
            // 
            this.tm.Interval = 500;
            this.tm.Tick += new System.EventHandler(this.tm_Tick);
            // 
            // flp
            // 
            this.flp.Location = new System.Drawing.Point(11, 146);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(152, 215);
            this.flp.TabIndex = 35;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 454);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureWeb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureWeb;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ComboBox comboWeb;
        private System.Windows.Forms.Button salvarBtn;
        private System.Windows.Forms.TextBox nomePessoa;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer tm;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flp;
    }
}

