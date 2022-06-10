using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace 柒幻_寒露
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        public static string opened_file_location = "";
        public static bool imported_file = false;
        readonly int item_display_length = 30;
        private string save_location = "";
        // Rtx
        readonly List<TabControl> tabControls = new List<TabControl>(2);
        readonly public static List<RichTextBox> rtxs = new List<RichTextBox>(2);
        public static int target_rtx = 0;
        int target_list = 0;
        // Problem
        public static List<string> questions = new List<string>();
        public static List<string> answers = new List<string>();
        // Find & Replace
        public static int FAR_type;
        // Settings
        public static string font_name;
        public static int font_size;
        public static string forecolor;
        public static string backcolor;
        readonly char problem_spliter = '\n';
        public static char QA_spliter;
        // Settings - Display
        public static bool display_default_font_and_size = false;
        public static int display_style = 0;
        public static bool random_display = false;
        public static int random_lenght = 0;

        public class Problem
        {
            readonly public static List<string> questions = new List<string>();
            readonly public static List<string> answers = new List<string>();
        }

        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1;
            byte curByte;
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("not expected byte format");
            }
            return true;
        }

        public static System.Text.Encoding GetType(FileStream fs)
        {
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF };
            Encoding reVal = Encoding.Default;
            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = Encoding.Unicode;
            }
            r.Close();
            return reVal;
        }

        public static System.Text.Encoding GetType(string FILE_NAME)
        {
            FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read);
            Encoding r = GetType(fs);
            fs.Close();
            return r;
        }

        private void SetMenu(int type = 0)
        {
            switch (type)
            {
                case 0:
                    保存SToolStripMenuItem.Enabled = false;
                    另存为AToolStripMenuItem.Enabled = false;
                    导出EToolStripMenuItem.Enabled = false;
                    查找FToolStripMenuItem.Enabled = false;
                    替换HToolStripMenuItem.Enabled = false;
                    撤销UToolStripMenuItem.Enabled = false;
                    重做RToolStripMenuItem.Enabled = false;
                    剪切TToolStripMenuItem.Enabled = false;
                    复制CToolStripMenuItem.Enabled = false;
                    粘贴PToolStripMenuItem.Enabled = false;
                    删除DToolStripMenuItem.Enabled = false;
                    全选AToolStripMenuItem.Enabled = false;
                    交换题干和答案内容ToolStripMenuItem.Enabled = false;
                    从头开始ToolStripMenuItem.Enabled = false;
                    从当前开始ToolStripMenuItem.Enabled = false;
                    随机乱序开始ToolStripMenuItem.Enabled = false;
                    break;
                case 1:
                    保存SToolStripMenuItem.Enabled = true;
                    另存为AToolStripMenuItem.Enabled = true;
                    导出EToolStripMenuItem.Enabled = true;
                    查找FToolStripMenuItem.Enabled = true;
                    替换HToolStripMenuItem.Enabled = true;
                    撤销UToolStripMenuItem.Enabled = true;
                    重做RToolStripMenuItem.Enabled = true;
                    剪切TToolStripMenuItem.Enabled = true;
                    复制CToolStripMenuItem.Enabled = true;
                    粘贴PToolStripMenuItem.Enabled = true;
                    删除DToolStripMenuItem.Enabled = true;
                    全选AToolStripMenuItem.Enabled = true;
                    交换题干和答案内容ToolStripMenuItem.Enabled = true;
                    从头开始ToolStripMenuItem.Enabled = true;
                    从当前开始ToolStripMenuItem.Enabled = true;
                    随机乱序开始ToolStripMenuItem.Enabled = true;
                    break;
            }
        }
        private void Frm_Main_Load(object sender, EventArgs e)
        {
            tabControls.Add(TabControl1);
            tabControls.Add(TabControl2);
            rtxs.Add(Rtx1);
            rtxs.Add(Rtx2);
            Rtx1.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            Rtx2.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            // Layout
            SetMenu();
            SplitContainer_Menu.SplitterDistance = Width / 5;
            SplitContainer_Rtxs.SplitterDistance = SplitContainer_Rtxs.Height / 2;
            // Settings
            try
            {
                font_name = ConfigurationManager.AppSettings["FontName"];
                font_size = Convert.ToInt32(ConfigurationManager.AppSettings["FontSize"]);
                forecolor = ConfigurationManager.AppSettings["ForeColor"];
                backcolor = ConfigurationManager.AppSettings["BackColor"];
                display_default_font_and_size = Convert.ToBoolean(ConfigurationManager.AppSettings["DefaultFont"]);
                display_style = Convert.ToInt32(ConfigurationManager.AppSettings["DisplayStyle"]);
                QA_spliter = Convert.ToChar(ConfigurationManager.AppSettings["QASpliter"]);
                rtxs.ForEach(rtx =>
                {
                    rtx.Font = new Font(font_name, font_size);
                    rtx.ForeColor = ColorTranslator.FromHtml(forecolor);
                    rtx.BackColor = ColorTranslator.FromHtml(backcolor);
                });
            }
            catch
            {
                ReportError("配置文件异常");
                Application.Exit();
                return;
            }
            AutoResize();
        }

        private void AutoResize()
        {
            Tx_Search.Width = ToolStrip_Menu.Width - 350;
            Tx_Search.SelectedText = ""; // bug fixed
        }

        private void Frm_Main_Resize(object sender, EventArgs e)
        {
            AutoResize();
        }

        public void SaveConfig()
        {
            XmlDocument doc = new XmlDocument();
            string strFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            doc.Load(strFileName);
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 1; i < nodes.Count; i++)
            {
                XmlAttribute att = nodes[i].Attributes["key"];
                switch (att.Value)
                {
                    case "FontName":
                        att = nodes[i].Attributes["value"];
                        att.Value = font_name;
                        break;
                    case "FontSize":
                        att = nodes[i].Attributes["value"];
                        att.Value = font_size.ToString();
                        break;
                    case "ForeColor":
                        att = nodes[i].Attributes["value"];
                        att.Value = forecolor;
                        break;
                    case "BackColor":
                        att = nodes[i].Attributes["value"];
                        att.Value = backcolor;
                        break;
                    case "DefaultFont":
                        att = nodes[i].Attributes["value"];
                        att.Value = display_default_font_and_size.ToString();
                        break;
                    case "DisplayStyle":
                        att = nodes[i].Attributes["value"];
                        att.Value = display_style.ToString();
                        break;
                    case "QASpliter":
                        att = nodes[i].Attributes["value"];
                        att.Value = QA_spliter.ToString();
                        break;
                }
            }
            doc.Save(strFileName);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void Pn_Welcome_Resize(object sender, EventArgs e)
        {
            Pn_Welcome_Controls.Location = new Point(Pn_Welcome.Width / 2 - Pn_Welcome_Controls.Width / 2, Pn_Welcome.Height / 2 - Pn_Welcome_Controls.Height / 2);
        }

        private void SetProblemStatus()
        {
            StatusStrip_SelectedProblem.Text = List_Problem.Items.Count != 0 ? string.Format("当前选中：第 {0} 项，共 {1} 项", List_Problem.SelectedIndex + 1, List_Problem.Items.Count) : "当前选中：无，共 0 项";
        }

        private void AddProblemToTreeview(string name = "新建题目")
        {
            //LoadProblemToTreeview();
            HidePnWelcome(); // debug
            questions.Add("");
            answers.Add("");
            List_Problem.Items.Add(name);
            List_Problem.SelectedIndex = List_Problem.Items.Count - 1;
            rtxs.ForEach(rtx => rtx.Text = "");
            SetMenu(1);
            SetProblemStatus();
        }

        private void RemoveAllProblemsFromTreeview()
        {
            questions.Clear();
            answers.Clear();
            List_Problem.Items.Clear();
        }

        private void SetRtxFocus(int target = 0)
        {
            rtxs[target_rtx = target].Focus();
            switch (target)
            {
                case 0:
                    SplitContainer_Rtxs.Panel1.BackColor = Color.FromArgb(153, 180, 209);
                    SplitContainer_Rtxs.Panel2.BackColor = Color.FromArgb(240, 240, 240);
                    break;
                case 1:
                    SplitContainer_Rtxs.Panel1.BackColor = Color.FromArgb(240, 240, 240);
                    SplitContainer_Rtxs.Panel2.BackColor = Color.FromArgb(153, 180, 209);
                    break;
            }
            ShowRank(rtxs[target_rtx]);
        }

        private void HidePnWelcome()
        {
            Pn_Welcome.Visible = false;
            SplitContainer_Rtxs.Visible = true;
        }

        private void Btn_Welcome_New_Click(object sender, EventArgs e)
        {
            HidePnWelcome();
            AddProblemToTreeview();
            SetRtxFocus();
        }

        private void Btn_Welcome_Open_Click(object sender, EventArgs e)
        {
            打开OToolStripMenuItem_Click(this, new EventArgs());
        }

        private void Btn_Welcome_Import_Click(object sender, EventArgs e)
        {
            导入为项目IToolStripMenuItem_Click(this, new EventArgs());
        }

        private void Rtx1_Click(object sender, EventArgs e)
        {
            SetRtxFocus();
        }

        private void Rtx2_Click(object sender, EventArgs e)
        {
            SetRtxFocus(1);
        }

        private void 项目PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HidePnWelcome();
            RemoveAllProblemsFromTreeview();
            AddProblemToTreeview();
            opened_file_location = save_location = "";
        }

        private void 题目QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProblemToTreeview();
            HidePnWelcome();
        }

        private void List_Problem_Click(object sender, EventArgs e)
        {
            if (List_Problem.Items.Count > 0)
            {
                Rtx1.Text = questions[List_Problem.SelectedIndex];
                Rtx2.Text = answers[List_Problem.SelectedIndex];
            }
        }

        private void Rtx1_TextChanged(object sender, EventArgs e)
        {
            SetRtxFocus();
            // Set problem title
            questions[List_Problem.SelectedIndex] = Rtx1.Text;
            // Insert problem item
            target_list = List_Problem.SelectedIndex;
            LoadProblemToTreeview();
            List_Problem.SelectedIndex = target_list;
            //target_list = List_Problem.SelectedIndex;
            //List_Problem.Items.RemoveAt(target_list);
            //List_Problem.Items.Insert(target_list, Rtx1.Text);
            //List_Problem.SelectedIndex = target_list;
        }

        private void Rtx2_TextChanged(object sender, EventArgs e)
        {
            SetRtxFocus(1);
            //answers[target_list] = Rtx2.Text;
            // Debug
            answers[List_Problem.SelectedIndex] = Rtx2.Text;
        }

        private void LoadProblemToTreeview()
        {
            HidePnWelcome();
            List_Problem.Items.Clear();
            foreach (var q in questions)
            {
                List_Problem.Items.Add(q == "" ? "新建题目" : (q.Length < item_display_length ? q : q.Substring(0, item_display_length) + "..."));
            }
        }

        private void OpenXML(string path = "")
        {
            try
            {
                if (path != "")
                {
                    opened_file_location = path;
                }
                else
                {
                    opened_file_location = "";
                    OpenFileDialog.Title = "打开";
                    OpenFileDialog.Filter = "xml文件(*.xml)|*.xml";
                    if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        opened_file_location = OpenFileDialog.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
                questions.Clear();
                answers.Clear();
                XmlDocument doc = new XmlDocument();
                doc.Load(opened_file_location);
                XmlNodeList protocol = doc.SelectSingleNode("Problems").ChildNodes;
                foreach (XmlNode p in protocol)
                {
                    questions.Add(p.FirstChild.InnerText.Replace("\"", ""));
                    answers.Add(p.LastChild.InnerText.Replace("\"", ""));
                }
                imported_file = true;
                LoadProblemToTreeview();
                SetMenu(1);
            }
            catch
            {
                ReportError("打开失败");
            }
        }

        private void ReportError(string errorMessage = "")
        {
            MessageBox.Show(errorMessage, "错误");
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenXML();
        }

        private void 导入为项目IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog.Title = "导入";
                OpenFileDialog.Filter = "文本文件(*.txt)|*.txt";
                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Encoding f_coding = GetType(OpenFileDialog.FileName);
                    StreamReader sr = new StreamReader(OpenFileDialog.FileName, f_coding);
                    string str = sr.ReadToEnd();
                    sr.Close();
                    string[] problems = str.Split(problem_spliter);
                    int n = problems.Length;
                    List<string> t_questions = new List<string>(n);
                    List<string> t_answers = new List<string>(n);
                    foreach (string p in problems)
                    {
                        string[] qa = p.Split(QA_spliter);
                        t_questions.Add(qa[0]);
                        t_answers.Add(qa[1] = qa[1].TrimStart());
                    }
                    questions = t_questions;
                    answers = t_answers;
                    LoadProblemToTreeview();
                    SetMenu(1);
                    imported_file = true;
                }
                else
                {
                    return;
                }
            }
            catch
            {
                ReportError("导入失败\n请检查文本内容格式是否与设置中的分隔符相匹配");
            }
        }

        private void SaveAsXml()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(dec);
                XmlNode root_protocol = doc.CreateElement("Problems");
                doc.AppendChild(root_protocol);
                for (int i = 0; i < questions.Count; i++)
                {
                    XmlElement root_protocolData = doc.CreateElement("Problem");
                    root_protocol.AppendChild(root_protocolData);
                    XmlElement root_question = doc.CreateElement("Question");
                    root_question.InnerText = questions[i];
                    root_protocolData.AppendChild(root_question);
                    XmlElement root_answer = doc.CreateElement("Answer");
                    root_answer.InnerText = answers[i];
                    root_protocolData.AppendChild(root_answer);
                }
                if (imported_file)
                {
                    doc.Save(opened_file_location);
                }
                else
                {
                    if (save_location == "")
                    {
                        SaveFileDialog.Filter = "xml文件(*.xml)|*.xml";
                        SaveFileDialog.FileName = "新建项目";
                        if (SaveFileDialog.ShowDialog() == DialogResult.OK && SaveFileDialog.FileName.Length > 0)
                        {
                            doc.Save(save_location = SaveFileDialog.FileName);
                        }
                    }
                    else
                    {
                        doc.Save(save_location);
                    }
                }
            }
            catch
            {
                ReportError("保存失败");
            }
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsXml();
        }

        private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsXml();
        }

        private void 导出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog.Filter = "文本文件(*.txt)|*.txt";
                SaveFileDialog.FileName = "新建文本文件";
                if (SaveFileDialog.ShowDialog() == DialogResult.OK && SaveFileDialog.FileName.Length > 0)
                {
                    FileStream fileStream = new FileStream(SaveFileDialog.FileName, FileMode.Append);
                    StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
                    StringBuilder str = new StringBuilder();
                    for (int i = 0; i < questions.Count; i++)
                    {
                        str.Append(questions[i] + QA_spliter + answers[i]);
                        if (i != questions.Count - 1)
                            str.Append('\n');
                    }
                    streamWriter.Write(str.ToString());
                    streamWriter.Flush();
                    streamWriter.Close();
                    fileStream.Close();
                }
            }
            catch
            {
                ReportError("导出文件失败");
            }
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowFARDialog(int type = 0)
        {
            FAR_type = type;
            new Frm_FAR().Show();
        }

        private void 查找FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFARDialog();
        }

        private void 替换HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFARDialog(1);
        }

        private RichTextBox GetEditTarget()
        {
            return target_rtx == 0 ? Rtx1 : Rtx2;
        }

        private void 撤销UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetEditTarget().Undo();
        }

        private void 重做RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetEditTarget().Redo();
        }

        private void 剪切TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetEditTarget().Cut();
        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(GetEditTarget().SelectedText, true);
        }

        private void 粘贴PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                Clipboard.SetDataObject(new RichTextBox
                {
                    Font = new Font(font_name, font_size),
                    ForeColor = ColorTranslator.FromHtml(forecolor),
                    BackColor = ColorTranslator.FromHtml(backcolor),
                    Text = Clipboard.GetText()
                }.Text, true); // Reset text format
                GetEditTarget().Paste();
            }
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetEditTarget().SelectedText = "";
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetEditTarget().SelectAll();
        }

        private void 题目管理器CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SplitContainer_Menu.Panel1Collapsed = !题目管理器CToolStripMenuItem.Checked;
        }

        private void 自动换行WToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxs.ForEach(p => p.WordWrap = 自动换行WToolStripMenuItem.Checked);
        }

        private void Display(int start = 0, bool rnd_display = false)
        {
            Frm_Display frm_Display = new Frm_Display
            {
                display_index = start
            };
            random_display = rnd_display;
            frm_Display.ShowDialog();
        }

        private void 从头开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void 从当前开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Display(Math.Max(0, List_Problem.SelectedIndex));
        }
        
        private void 随机乱序开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            random_lenght = List_Problem.Items.Count;
            Display(0, true);
        }

        private void 选项OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Frm_Option().ShowDialog();
        }

        private void 关于柒幻寒露AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Frm_About().ShowDialog();
        }

        private void ShowRank(RichTextBox rtb)
        {
            int index = rtb.GetFirstCharIndexOfCurrentLine();
            int line = rtb.GetLineFromCharIndex(index) + 1;
            int column = rtb.SelectionStart - index + 1;
            StatusStrip_RAC.Text = string.Format("行：{0}  列：{1}", line.ToString(), column.ToString());
        }

        private void ExchangeProblem(int index1, int index2)
        {
            string t = questions[index1];
            questions[index1] = questions[index2];
            questions[index2] = t;
            t = answers[index1];
            answers[index1] = answers[index2];
            answers[index2] = t;
            LoadProblemToTreeview();
            List_Problem.SelectedIndex = index2;
        }
        private void Ts_Open_Click(object sender, EventArgs e)
        {
            打开OToolStripMenuItem_Click(this, new EventArgs());
        }

        private void Ts_Import_Click(object sender, EventArgs e)
        {
            导入为项目IToolStripMenuItem_Click(this, new EventArgs());
        }

        private void 添加AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProblemToTreeview();
        }

        private void SetDisplayEnable(bool status)
        {
            从头开始ToolStripMenuItem.Enabled = 从当前开始ToolStripMenuItem.Enabled = 随机乱序开始ToolStripMenuItem.Enabled = 保存SToolStripMenuItem.Enabled = 另存为AToolStripMenuItem.Enabled = 导出EToolStripMenuItem.Enabled = status;
        }

        private void 删除XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (List_Problem.SelectedIndex != -1)
            {
                questions.RemoveAt(List_Problem.SelectedIndex);
                answers.RemoveAt(List_Problem.SelectedIndex);
                LoadProblemToTreeview();
                if (List_Problem.Items.Count > 0)
                {
                    List_Problem.SelectedIndex = 0;
                }
                SetProblemStatus();
                SetDisplayEnable(List_Problem.Items.Count != 0);
            }
        }

        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (List_Problem.SelectedIndex != -1)
            {
                int index = List_Problem.SelectedIndex;
                ExchangeProblem(index, index < 1 ? List_Problem.Items.Count - 1 : index - 1);
            }
        }

        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (List_Problem.SelectedIndex!= -1)
            {
                int index = List_Problem.SelectedIndex;
                ExchangeProblem(index, index + 1 == List_Problem.Items.Count ? 0 : index + 1);
            }
        }

        private void ContextMenuStrip_Problem_Opened(object sender, EventArgs e)
        {
            上移UToolStripMenuItem.Enabled = 下移DToolStripMenuItem.Enabled = 删除XToolStripMenuItem.Enabled = List_Problem.Items.Count > 1;
        }

        private void Ts_AddProblem_Click(object sender, EventArgs e)
        {
            AddProblemToTreeview();
        }

        private void Ts_DeleteProblem_Click(object sender, EventArgs e)
        {
            if (List_Problem.SelectedIndex != -1)
            {
                删除XToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void Ts_Up_Click(object sender, EventArgs e)
        {
            if (List_Problem.SelectedIndex != -1)
            {
                上移ToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void Ts_Down_Click(object sender, EventArgs e)
        {
            if (List_Problem.SelectedIndex != -1)
            {
                下移ToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void List_Problem_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetProblemStatus();
        }

        private void Ts_Cut_Click(object sender, EventArgs e)
        {
            if (!Pn_Welcome.Visible)
            {
                剪切TToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void Ts_Copy_Click(object sender, EventArgs e)
        {
            if (!Pn_Welcome.Visible)
            {
                复制CToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void Ts_Paste_Click(object sender, EventArgs e)
        {
            if (!Pn_Welcome.Visible)
            {
                粘贴PToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void Ts_Delete_Click(object sender, EventArgs e)
        {
            if (!Pn_Welcome.Visible)
            {
                删除DToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void Ts_Undo_Click(object sender, EventArgs e)
        {
            if (!Pn_Welcome.Visible)
            {
                撤销UToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void Ts_Redo_Click(object sender, EventArgs e)
        {
            if (!Pn_Welcome.Visible)
            {
                重做RToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void Ts_Search_Click(object sender, EventArgs e)
        {
            if (!Pn_Welcome.Visible)
            {
                查找FToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void Ts_Replace_Click(object sender, EventArgs e)
        {
            if (!Pn_Welcome.Visible)
            {
                替换HToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void 打开项目OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            打开OToolStripMenuItem_Click(this, new EventArgs());
        }

        private void 导入为项目IToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            导入为项目IToolStripMenuItem_Click(this, new EventArgs());
        }

        private void SplitContainer_Menu_Panel2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void SplitContainer_Menu_Panel2_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            OpenXML(path);
        }

        private void 交换题干和答案内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string t = Rtx1.Text;
            Rtx1.Text = Rtx2.Text;
            Rtx2.Text = t;
        }

        private void 新建项目NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HidePnWelcome();
            RemoveAllProblemsFromTreeview();
            AddProblemToTreeview();
            opened_file_location = save_location = "";
        }

        private void 添加题目PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProblemToTreeview();
            HidePnWelcome();
        }

        private void 快速导入题目QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                ReportError("剪切板中无内容");
                return;
            }
            Clipboard.SetDataObject(new RichTextBox
            {
                Font = new Font(font_name, font_size),
                ForeColor = ColorTranslator.FromHtml(forecolor),
                BackColor = ColorTranslator.FromHtml(backcolor),
                Text = Clipboard.GetText()
            }.Text, true); // Reset text format
            string problem = Clipboard.GetText();
            if (problem.Contains(QA_spliter))
            {
                string[] qa = problem.Split(QA_spliter);
                if (qa.Length != 2)
                {
                    ReportError("快速导入内容无效");
                    return;
                }
                AddProblemToTreeview();
                HidePnWelcome();
                int last = questions.Count - 1;
                Rtx1.Text = questions[last] = qa[0];
                Rtx2.Text = answers[last] = qa[1];
            }
            else
            {
                ReportError("未检测到分隔符");
            }
        }

        private void Tx_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                for (int i = List_Problem.SelectedItem != null ? List_Problem.SelectedIndex + 1 : 0; i < List_Problem.Items.Count; i++)
                {
                    if (List_Problem.Items[i].ToString().Contains(Tx_Search.Text))
                    {
                        List_Problem.SelectedIndex = i;
                        return;
                    }
                }
                ReportError("未找到或查找到末尾");
            }
        }

        private void SplitContainer_Menu_Panel1_Resize(object sender, EventArgs e)
        {
            AutoResize();
        }
    }
}
