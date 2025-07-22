namespace RssReader {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            tbUrl = new ComboBox();
            btRssGet = new Button();
            lbTitles = new ListBox();
            wvRssLink = new Microsoft.Web.WebView2.WinForms.WebView2();
            btGoBack = new Button();
            btGoForward = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)wvRssLink).BeginInit();
            SuspendLayout();
            // 
            // tbUrl
            // 
            tbUrl.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbUrl.Location = new Point(172, 13);
            tbUrl.Name = "tbUrl";
            tbUrl.Size = new Size(503, 33);
            tbUrl.TabIndex = 0;
            // 
            // btRssGet
            // 
            btRssGet.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssGet.Location = new Point(681, 12);
            btRssGet.Name = "btRssGet";
            btRssGet.Size = new Size(78, 34);
            btRssGet.TabIndex = 1;
            btRssGet.Text = "取得";
            btRssGet.UseVisualStyleBackColor = true;
            btRssGet.Click += btRssGet_Click;
            // 
            // lbTitles
            // 
            lbTitles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbTitles.FormattingEnabled = true;
            lbTitles.ItemHeight = 15;
            lbTitles.Location = new Point(12, 106);
            lbTitles.Name = "lbTitles";
            lbTitles.Size = new Size(747, 169);
            lbTitles.TabIndex = 2;
            lbTitles.Click += lbTitles_Click;
            // 
            // wvRssLink
            // 
            wvRssLink.AllowExternalDrop = true;
            wvRssLink.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            wvRssLink.CreationProperties = null;
            wvRssLink.DefaultBackgroundColor = Color.White;
            wvRssLink.Location = new Point(12, 281);
            wvRssLink.Name = "wvRssLink";
            wvRssLink.Size = new Size(747, 342);
            wvRssLink.TabIndex = 3;
            wvRssLink.ZoomFactor = 1D;
            wvRssLink.NavigationCompleted += wvRssLink_NavigationCompleted;
            wvRssLink.SourceChanged += wvRssLink_SourceChanged;
            // 
            // btGoBack
            // 
            btGoBack.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btGoBack.Location = new Point(12, 13);
            btGoBack.Name = "btGoBack";
            btGoBack.Size = new Size(76, 33);
            btGoBack.TabIndex = 4;
            btGoBack.Text = "戻る";
            btGoBack.UseVisualStyleBackColor = true;
            btGoBack.Click += btGoBack_Click;
            // 
            // btGoForward
            // 
            btGoForward.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btGoForward.Location = new Point(90, 13);
            btGoForward.Name = "btGoForward";
            btGoForward.Size = new Size(76, 33);
            btGoForward.TabIndex = 4;
            btGoForward.Text = "進む";
            btGoForward.UseVisualStyleBackColor = true;
            btGoForward.Click += btGoForward_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            button1.Location = new Point(681, 52);
            button1.Name = "button1";
            button1.Size = new Size(78, 32);
            button1.TabIndex = 5;
            button1.Text = "登録";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(172, 58);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(503, 23);
            textBox1.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(779, 635);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(btGoForward);
            Controls.Add(btGoBack);
            Controls.Add(wvRssLink);
            Controls.Add(lbTitles);
            Controls.Add(btRssGet);
            Controls.Add(tbUrl);
            Name = "Form1";
            Text = "RSSリーダー";
            ((System.ComponentModel.ISupportInitialize)wvRssLink).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox tbUrl;
        private Button btRssGet;
        private ListBox lbTitles;
        private Microsoft.Web.WebView2.WinForms.WebView2 wvRssLink;
        private Button btGoBack;
        private Button btGoForward;
        private Button button1;
        private TextBox textBox1;
    }
}
