
namespace 柒幻_寒露
{
    partial class Frm_Display
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
            this.Lbl_Control_Title = new System.Windows.Forms.Label();
            this.Lbl_Question = new System.Windows.Forms.Label();
            this.Lbl_Control_Page = new System.Windows.Forms.Label();
            this.Lbl_Answer = new System.Windows.Forms.Label();
            this.ContextMenuStrip_Main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.下一张NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上一张PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.结束放映EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Pn_Question = new System.Windows.Forms.Panel();
            this.Pn_Answer = new System.Windows.Forms.Panel();
            this.ContextMenuStrip_Main.SuspendLayout();
            this.Pn_Question.SuspendLayout();
            this.Pn_Answer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lbl_Control_Title
            // 
            this.Lbl_Control_Title.AutoSize = true;
            this.Lbl_Control_Title.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lbl_Control_Title.Location = new System.Drawing.Point(456, 3);
            this.Lbl_Control_Title.Name = "Lbl_Control_Title";
            this.Lbl_Control_Title.Size = new System.Drawing.Size(75, 24);
            this.Lbl_Control_Title.TabIndex = 1;
            this.Lbl_Control_Title.Text = "Title";
            this.Lbl_Control_Title.Visible = false;
            // 
            // Lbl_Question
            // 
            this.Lbl_Question.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Lbl_Question.AutoSize = true;
            this.Lbl_Question.Location = new System.Drawing.Point(446, 242);
            this.Lbl_Question.Margin = new System.Windows.Forms.Padding(10);
            this.Lbl_Question.Name = "Lbl_Question";
            this.Lbl_Question.Size = new System.Drawing.Size(106, 24);
            this.Lbl_Question.TabIndex = 0;
            this.Lbl_Question.Text = "Question";
            this.Lbl_Question.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.Lbl_Question.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Lbl_Question_MouseClick);
            // 
            // Lbl_Control_Page
            // 
            this.Lbl_Control_Page.AutoSize = true;
            this.Lbl_Control_Page.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lbl_Control_Page.Location = new System.Drawing.Point(449, 250);
            this.Lbl_Control_Page.Name = "Lbl_Control_Page";
            this.Lbl_Control_Page.Size = new System.Drawing.Size(62, 24);
            this.Lbl_Control_Page.TabIndex = 2;
            this.Lbl_Control_Page.Text = "Page";
            this.Lbl_Control_Page.Visible = false;
            // 
            // Lbl_Answer
            // 
            this.Lbl_Answer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Lbl_Answer.AutoSize = true;
            this.Lbl_Answer.Location = new System.Drawing.Point(449, 10);
            this.Lbl_Answer.Margin = new System.Windows.Forms.Padding(10);
            this.Lbl_Answer.Name = "Lbl_Answer";
            this.Lbl_Answer.Size = new System.Drawing.Size(82, 24);
            this.Lbl_Answer.TabIndex = 0;
            this.Lbl_Answer.Text = "Answer";
            this.Lbl_Answer.Visible = false;
            this.Lbl_Answer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Lbl_Answer_MouseClick);
            // 
            // ContextMenuStrip_Main
            // 
            this.ContextMenuStrip_Main.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ContextMenuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.下一张NToolStripMenuItem,
            this.上一张PToolStripMenuItem,
            this.toolStripSeparator1,
            this.结束放映EToolStripMenuItem});
            this.ContextMenuStrip_Main.Name = "ContextMenuStrip_Main";
            this.ContextMenuStrip_Main.Size = new System.Drawing.Size(214, 124);
            // 
            // 下一张NToolStripMenuItem
            // 
            this.下一张NToolStripMenuItem.Name = "下一张NToolStripMenuItem";
            this.下一张NToolStripMenuItem.Size = new System.Drawing.Size(213, 38);
            this.下一张NToolStripMenuItem.Text = "下一张(&N)";
            this.下一张NToolStripMenuItem.Click += new System.EventHandler(this.下一张NToolStripMenuItem_Click);
            // 
            // 上一张PToolStripMenuItem
            // 
            this.上一张PToolStripMenuItem.Name = "上一张PToolStripMenuItem";
            this.上一张PToolStripMenuItem.Size = new System.Drawing.Size(213, 38);
            this.上一张PToolStripMenuItem.Text = "上一张(&P)";
            this.上一张PToolStripMenuItem.Click += new System.EventHandler(this.上一张PToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(210, 6);
            // 
            // 结束放映EToolStripMenuItem
            // 
            this.结束放映EToolStripMenuItem.Name = "结束放映EToolStripMenuItem";
            this.结束放映EToolStripMenuItem.Size = new System.Drawing.Size(213, 38);
            this.结束放映EToolStripMenuItem.Text = "结束放映(&E)";
            this.结束放映EToolStripMenuItem.Click += new System.EventHandler(this.结束放映EToolStripMenuItem_Click);
            // 
            // Pn_Question
            // 
            this.Pn_Question.ContextMenuStrip = this.ContextMenuStrip_Main;
            this.Pn_Question.Controls.Add(this.Lbl_Question);
            this.Pn_Question.Controls.Add(this.Lbl_Control_Title);
            this.Pn_Question.Location = new System.Drawing.Point(40, 7);
            this.Pn_Question.Name = "Pn_Question";
            this.Pn_Question.Size = new System.Drawing.Size(1013, 276);
            this.Pn_Question.TabIndex = 1;
            this.Pn_Question.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Pn_Question_MouseClick);
            // 
            // Pn_Answer
            // 
            this.Pn_Answer.ContextMenuStrip = this.ContextMenuStrip_Main;
            this.Pn_Answer.Controls.Add(this.Lbl_Control_Page);
            this.Pn_Answer.Controls.Add(this.Lbl_Answer);
            this.Pn_Answer.Location = new System.Drawing.Point(40, 358);
            this.Pn_Answer.Name = "Pn_Answer";
            this.Pn_Answer.Size = new System.Drawing.Size(1013, 291);
            this.Pn_Answer.TabIndex = 2;
            this.Pn_Answer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Pn_Answer_MouseClick);
            // 
            // Frm_Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 709);
            this.Controls.Add(this.Pn_Answer);
            this.Controls.Add(this.Pn_Question);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Display";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Display_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_Display_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Frm_Display_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Frm_Display_MouseClick);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Frm_Display_MouseWheel);
            this.ContextMenuStrip_Main.ResumeLayout(false);
            this.Pn_Question.ResumeLayout(false);
            this.Pn_Question.PerformLayout();
            this.Pn_Answer.ResumeLayout(false);
            this.Pn_Answer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label Lbl_Question;
        private System.Windows.Forms.Label Lbl_Answer;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Main;
        private System.Windows.Forms.ToolStripMenuItem 下一张NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上一张PToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 结束放映EToolStripMenuItem;
        private System.Windows.Forms.Label Lbl_Control_Title;
        private System.Windows.Forms.Label Lbl_Control_Page;
        private System.Windows.Forms.Panel Pn_Question;
        private System.Windows.Forms.Panel Pn_Answer;
    }
}