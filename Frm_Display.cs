using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 柒幻_寒露
{
    public partial class Frm_Display : Form
    {
        public Frm_Display()
        {
            InitializeComponent();
        }

        public int display_index = 0;
        bool display_answer = true;
        // Layout
        int p; // this.Padding
        int lbl_display_width;
        int lbl_display_height;
        int split_distance;
        int half_width; // Width / 2
        // Random Display
        List<int> random_array = new List<int>();
        int random_begin_index;
        int random_end_index;
        // Control
        bool control_status = false;

        private void Frm_Display_Load(object sender, EventArgs e)
        {
            // Random Display
            if (Frm_Main.random_display)
            {
                int n = Frm_Main.random_lenght;
                for (int i = 0; i < n; i++)
                {
                    random_array.Add(i);
                }
                random_array = RandomSampling(random_array, n, n, false);
                random_begin_index = random_array[0];
                random_end_index = random_array[random_array.Count - 1];
            }
            // Layout
            p = Padding.All;
            lbl_display_width = Width - p - p;
            lbl_display_height = Height / 2 - p - p;
            Pn_Question.Size = Pn_Answer.Size = new Size(lbl_display_width, lbl_display_height);
            Pn_Question.Location = new Point(p, p);
            Pn_Answer.Location = new Point(p, p + p + p + lbl_display_height);
            split_distance = p + p;
            half_width = Width / 2;
            // Style
            switch (Frm_Main.display_default_font_and_size)
            {
                case true:
                    Lbl_Question.Font = Lbl_Answer.Font = new Font("宋体", 18);
                    break;
                case false:
                    Lbl_Question.Font = Lbl_Answer.Font = new Font(Frm_Main.font_name, Frm_Main.font_size);
                    break;
            }
            switch (Frm_Main.display_style)
            {
                case 0:
                    Lbl_Question.ForeColor = Color.Black;
                    break;
                case 1:
                    Lbl_Question.ForeColor = Color.FromArgb(136, 171, 218);
                    break;
            }
            // Debug: Hide SplitContainer's splitter
            //Lbl_Question.Focus();
            //SplitContainer_Main.Focus();
            // End
            Display(0);
        }

        private List<int> RandomSampling(List<int> nsp, int n, int m, bool repeat)
        {
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            List<int> res = new List<int>();
            int[] id = new int[n];
            if (repeat)
            {
                for (int a = 0; a < m; a++)
                {
                    res.Add(nsp[rand.Next(n)]);
                }
            }
            else
            {
                for (int a = 0; a < n; a++) id[a] = a;
                for (int a = 0, b; a < m; a++)
                {
                    b = rand.Next(n);
                    res.Add(nsp[id[b]]);
                    id[b] = id[--n];
                }
            }
            return res;
        }

        private void SetQALayout(int idx)
        {
            // Lbl_Question
            Lbl_Question.Text = Frm_Main.questions[idx];
            Lbl_Question.AutoSize = true;
            if (Lbl_Question.Width > lbl_display_width)
            {
                Lbl_Question.AutoSize = false;
                Lbl_Question.Size = new Size(lbl_display_width, lbl_display_height);
            }
            Lbl_Question.Location = new Point(half_width - Lbl_Question.Width / 2, lbl_display_height - Lbl_Question.Height - 10);
            // Lbl_Answer
            Lbl_Answer.Visible = !display_answer;
            Lbl_Answer.Text = Frm_Main.answers[idx];
            Lbl_Answer.AutoSize = true;
            if (Lbl_Answer.Width > lbl_display_width)
            {
                Lbl_Answer.AutoSize = false;
                Lbl_Answer.Size = new Size(lbl_display_width, lbl_display_height);
            }
            Lbl_Answer.Location = new Point(half_width - Lbl_Answer.Width / 2, 10);
        }

        private void Display(int direction)
        {
            if (!Frm_Main.random_display)
            {
                if (display_index == Frm_Main.questions.Count - 1 && display_answer && direction > 0)
                {
                    Close();
                    return;
                }
                else if (display_index == 0 && !display_answer && direction < 0)
                {
                    return;
                }
                if ((display_answer && direction > 0) || (!display_answer && direction < 0))
                {
                    display_index += direction;
                }
                SetQALayout(display_index);
            }
            else
            {
                if (random_array[display_index] == random_end_index && display_answer && direction > 0)
                {
                    Close();
                    return;
                }
                else if (random_array[display_index] == random_begin_index && !display_answer && direction < 0)
                {
                    return;
                }
                if ((display_answer && direction > 0) || (!display_answer && direction < 0))
                {
                    display_index += direction;
                }
                SetQALayout(random_array[display_index]);
            }
            // Check Answer
            display_answer = !display_answer;
        }

        private void MouseClickOnDisplay(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    Display(1);
                    break;
                case MouseButtons.Right:
                    ContextMenuStrip_Main.Show(new Point(MousePosition.X, MousePosition.Y));
                    break;
            }
        }

        private void Pn_Question_MouseClick(object sender, MouseEventArgs e)
        {
            MouseClickOnDisplay(e);
        }

        private void Pn_Answer_MouseClick(object sender, MouseEventArgs e)
        {
            MouseClickOnDisplay(e);
        }

        private void Lbl_Question_MouseClick(object sender, MouseEventArgs e)
        {
            MouseClickOnDisplay(e);
        }

        private void Lbl_Answer_MouseClick(object sender, MouseEventArgs e)
        {
            MouseClickOnDisplay(e);
        }

        private void ControlView(bool status = true)
        {
            Lbl_Control_Title.BringToFront();
            Lbl_Control_Page.BringToFront();
            Lbl_Control_Title.Visible = Lbl_Control_Page.Visible = status;
            control_status = status;
            Lbl_Control_Title.Text = (Frm_Main.random_display ? "随机乱序播放：" : "顺序播放：") + (Frm_Main.imported_file ? "导入项目" : Frm_Main.opened_file_location == "" ? "新建项目" : Frm_Main.opened_file_location);
            Lbl_Control_Title.Location = new Point(half_width - Lbl_Control_Title.Width / 2, p);
            Lbl_Control_Page.Text = "当前浏览：第" + (Frm_Main.random_display ? random_array[display_index] + 1: display_index + 1) + "页，共" + (Frm_Main.questions.Count) + "页" + (display_answer ? "（显示答案）" : "");
            Lbl_Control_Page.Location = new Point(half_width - Lbl_Control_Page.Width / 2, Pn_Answer.Height - Lbl_Control_Page.Height - p);
        }

        private void Frm_Display_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Up:
                case Keys.Left:
                case Keys.Back:
                    Display(-1);
                    break;
                case Keys.Down:
                case Keys.Right:
                case Keys.Enter:
                    Display(1);
                    break;
                case Keys.ControlKey:
                    ControlView();
                    break;
            }
        }

        private void Frm_Display_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Display(-1);
            }
            else if (e.Delta < 0)
            {
                Display(1);
            }
        }

        private void 下一张NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Display(1);
        }

        private void 上一张PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Display(-1);
        }

        private void 结束放映EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Main.random_display = false;
            Close();
        }
        private void Frm_Display_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                ControlView(false);
            }
        }

        private void Frm_Display_MouseClick(object sender, MouseEventArgs e)
        {
            MouseClickOnDisplay(e);
        }

    }
}
