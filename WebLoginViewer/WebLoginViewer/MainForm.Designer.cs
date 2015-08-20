namespace WebLoginViewer
{
    partial class WebLoginViewerForm
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
            this.introLabel = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openFileButton = new System.Windows.Forms.Button();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.totalLoginLabel = new System.Windows.Forms.Label();
            this.webNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.loginCountColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.isOnlineColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // introLabel
            // 
            this.introLabel.AutoSize = true;
            this.introLabel.Location = new System.Drawing.Point(13, 13);
            this.introLabel.Name = "introLabel";
            this.introLabel.Size = new System.Drawing.Size(104, 13);
            this.introLabel.TabIndex = 0;
            this.introLabel.Text = "Please select the file";
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(123, 8);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(75, 23);
            this.openFileButton.TabIndex = 1;
            this.openFileButton.Text = "Open File";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(204, 13);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(37, 13);
            this.fileNameLabel.TabIndex = 2;
            this.fileNameLabel.Text = "No file";
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.webNameColumnHeader,
            this.loginCountColumnHeader,
            this.isOnlineColumnHeader});
            this.listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView.Location = new System.Drawing.Point(16, 37);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(583, 512);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(609, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Total Login Credential";
            // 
            // totalLoginLabel
            // 
            this.totalLoginLabel.AutoSize = true;
            this.totalLoginLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalLoginLabel.Location = new System.Drawing.Point(605, 57);
            this.totalLoginLabel.Name = "totalLoginLabel";
            this.totalLoginLabel.Size = new System.Drawing.Size(42, 46);
            this.totalLoginLabel.TabIndex = 5;
            this.totalLoginLabel.Text = "0";
            // 
            // webNameColumnHeader
            // 
            this.webNameColumnHeader.Text = "Website";
            // 
            // loginCountColumnHeader
            // 
            this.loginCountColumnHeader.Text = "Login Count";
            // 
            // WebLoginViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.totalLoginLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.introLabel);
            this.Name = "WebLoginViewerForm";
            this.Text = "Web Login Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label introLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalLoginLabel;
        private System.Windows.Forms.ColumnHeader webNameColumnHeader;
        private System.Windows.Forms.ColumnHeader loginCountColumnHeader;
        private System.Windows.Forms.ColumnHeader isOnlineColumnHeader;
    }
}

