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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureWeb = new System.Windows.Forms.PictureBox();
            this.nomePessoa = new System.Windows.Forms.TextBox();
            this.showPessoas = new System.Windows.Forms.Button();
            this.salvarBtn = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.comboWeb = new System.Windows.Forms.ComboBox();
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
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureWeb);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.nomePessoa);
            this.splitContainer1.Panel2.Controls.Add(this.showPessoas);
            this.splitContainer1.Panel2.Controls.Add(this.salvarBtn);
            this.splitContainer1.Panel2.Controls.Add(this.startButton);
            this.splitContainer1.Panel2.Controls.Add(this.comboWeb);
            this.splitContainer1.Size = new System.Drawing.Size(1179, 559);
            this.splitContainer1.SplitterDistance = 946;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureWeb
            // 
            this.pictureWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureWeb.Location = new System.Drawing.Point(0, 0);
            this.pictureWeb.Name = "pictureWeb";
            this.pictureWeb.Size = new System.Drawing.Size(946, 559);
            this.pictureWeb.TabIndex = 0;
            this.pictureWeb.TabStop = false;
            // 
            // nomePessoa
            // 
            this.nomePessoa.Location = new System.Drawing.Point(12, 208);
            this.nomePessoa.Name = "nomePessoa";
            this.nomePessoa.Size = new System.Drawing.Size(204, 22);
            this.nomePessoa.TabIndex = 11;
            // 
            // showPessoas
            // 
            this.showPessoas.Location = new System.Drawing.Point(10, 268);
            this.showPessoas.Name = "showPessoas";
            this.showPessoas.Size = new System.Drawing.Size(209, 23);
            this.showPessoas.TabIndex = 7;
            this.showPessoas.Text = "Pessoas";
            this.showPessoas.UseVisualStyleBackColor = true;
            this.showPessoas.Click += new System.EventHandler(this.showPessoas_Click);
            // 
            // salvarBtn
            // 
            this.salvarBtn.Location = new System.Drawing.Point(7, 236);
            this.salvarBtn.Name = "salvarBtn";
            this.salvarBtn.Size = new System.Drawing.Size(209, 23);
            this.salvarBtn.TabIndex = 5;
            this.salvarBtn.Text = "Salvar";
            this.salvarBtn.UseVisualStyleBackColor = true;
            this.salvarBtn.Click += new System.EventHandler(this.salvarBtn_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(2, 42);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(214, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // comboWeb
            // 
            this.comboWeb.FormattingEnabled = true;
            this.comboWeb.Location = new System.Drawing.Point(3, 12);
            this.comboWeb.Name = "comboWeb";
            this.comboWeb.Size = new System.Drawing.Size(213, 24);
            this.comboWeb.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 559);
            this.Controls.Add(this.splitContainer1);
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
        private System.Windows.Forms.Button showPessoas;
        private System.Windows.Forms.TextBox nomePessoa;
    }
}

