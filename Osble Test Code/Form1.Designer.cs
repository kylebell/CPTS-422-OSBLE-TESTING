namespace Osble_Test_Code
{
    partial class wSignIn
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
            this.usrTextBox = new System.Windows.Forms.TextBox();
            this.pwdTextBox = new System.Windows.Forms.TextBox();
            this.pwdLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.resLabel = new System.Windows.Forms.Label();
            this.signButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usrTextBox
            // 
            this.usrTextBox.Location = new System.Drawing.Point(439, 73);
            this.usrTextBox.Name = "usrTextBox";
            this.usrTextBox.Size = new System.Drawing.Size(205, 20);
            this.usrTextBox.TabIndex = 1;
            // 
            // pwdTextBox
            // 
            this.pwdTextBox.Location = new System.Drawing.Point(439, 108);
            this.pwdTextBox.Name = "pwdTextBox";
            this.pwdTextBox.PasswordChar = '*';
            this.pwdTextBox.Size = new System.Drawing.Size(204, 20);
            this.pwdTextBox.TabIndex = 2;
            // 
            // pwdLabel
            // 
            this.pwdLabel.AutoSize = true;
            this.pwdLabel.Location = new System.Drawing.Point(380, 111);
            this.pwdLabel.Name = "pwdLabel";
            this.pwdLabel.Size = new System.Drawing.Size(53, 13);
            this.pwdLabel.TabIndex = 3;
            this.pwdLabel.Text = "Password";
            this.pwdLabel.Click += new System.EventHandler(this.pwdLabel_Click);
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(380, 76);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(55, 13);
            this.userLabel.TabIndex = 4;
            this.userLabel.Text = "Username";
            this.userLabel.Click += new System.EventHandler(this.usrLabel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(471, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 39);
            this.label3.TabIndex = 5;
            this.label3.Text = "OSBLE";
            // 
            // resLabel
            // 
            this.resLabel.AutoSize = true;
            this.resLabel.Location = new System.Drawing.Point(15, 180);
            this.resLabel.Name = "resLabel";
            this.resLabel.Size = new System.Drawing.Size(195, 13);
            this.resLabel.TabIndex = 6;
            this.resLabel.Text = "Server Response Should show up here.";
            // 
            // signButton
            // 
            this.signButton.Location = new System.Drawing.Point(439, 134);
            this.signButton.Name = "signButton";
            this.signButton.Size = new System.Drawing.Size(84, 26);
            this.signButton.TabIndex = 7;
            this.signButton.Text = "Sign-In";
            this.signButton.UseVisualStyleBackColor = true;
            this.signButton.Click += new System.EventHandler(this.signButton_Click);
            // 
            // wSignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 429);
            this.Controls.Add(this.signButton);
            this.Controls.Add(this.resLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.pwdLabel);
            this.Controls.Add(this.pwdTextBox);
            this.Controls.Add(this.usrTextBox);
            this.Name = "wSignIn";
            this.Text = "OSBLE Sign-in";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usrTextBox;
        private System.Windows.Forms.TextBox pwdTextBox;
        private System.Windows.Forms.Label pwdLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label resLabel;
        private System.Windows.Forms.Button signButton;
    }
}

