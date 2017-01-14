namespace WindowsFormsApplicationEpikryza
{
    partial class FormModel
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
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.labelk = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel17.SuspendLayout();
            this.panel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel17.Controls.Add(this.pictureBox1);
            this.panel17.Controls.Add(this.panel18);
            this.panel17.Location = new System.Drawing.Point(0, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(846, 717);
            this.panel17.TabIndex = 71;
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel18.Controls.Add(this.labelk);
            this.panel18.Location = new System.Drawing.Point(0, 1);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(889, 26);
            this.panel18.TabIndex = 29;
            // 
            // labelk
            // 
            this.labelk.AutoSize = true;
            this.labelk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelk.Location = new System.Drawing.Point(4, 5);
            this.labelk.Name = "labelk";
            this.labelk.Size = new System.Drawing.Size(41, 13);
            this.labelk.TabIndex = 0;
            this.labelk.Text = "Model";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WindowsFormsApplicationEpikryza.Properties.Resources.skeleton_img;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(171, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(472, 554);
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // FormModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(862, 678);
            this.Controls.Add(this.panel17);
            this.Name = "FormModel";
            this.Text = "FormModel";
            this.panel17.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Label labelk;
    }
}