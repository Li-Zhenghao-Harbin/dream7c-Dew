using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 柒幻_寒露
{
    public partial class Frm_Option : Form
    {
        public Frm_Option()
        {
            InitializeComponent();
        }

        protected string folder_path;

        private void GetFontNamesFromSystem()
        {
            FontFamily[] fontFamilies;
            Combo_Font.Items.Clear();
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            fontFamilies = installedFontCollection.Families;
            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                Combo_Font.Items.Add(fontFamilies[j].Name);
            }
        }

        private void Frm_Option_Load(object sender, EventArgs e)
        {
            GetFontNamesFromSystem();
            Combo_Font.SelectedItem = Frm_Main.font_name;
            Num_Font_Size.Value = Frm_Main.font_size;
            Btn_Forecolor.BackColor = ColorTranslator.FromHtml(Frm_Main.forecolor);
            Btn_Backcolor.BackColor = ColorTranslator.FromHtml(Frm_Main.backcolor);
            Check_DefaultFontAndSize.Checked = Frm_Main.display_default_font_and_size;
            Combo_DisplayStyle.SelectedIndex = Frm_Main.display_style;
            Tx_QASpliter.Text = Frm_Main.QA_spliter.ToString();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Confirm_Click(object sender, EventArgs e)
        {
            Frm_Main.font_name = Combo_Font.SelectedItem.ToString();
            Frm_Main.font_size = (int)Num_Font_Size.Value;
            Frm_Main.forecolor = ColorTranslator.ToHtml(Btn_Forecolor.BackColor);
            Frm_Main.backcolor = ColorTranslator.ToHtml(Btn_Backcolor.BackColor);
            Frm_Main.display_default_font_and_size = Check_DefaultFontAndSize.Checked;
            Frm_Main.display_style = Combo_DisplayStyle.SelectedIndex;
            Frm_Main.QA_spliter = Tx_QASpliter.Text[0];
            Frm_Main.rtxs.ForEach(rtx =>
            {
                rtx.Font = new Font(Frm_Main.font_name, Frm_Main.font_size);
                rtx.ForeColor = Btn_Forecolor.BackColor;
                rtx.BackColor = Btn_Backcolor.BackColor;
            });
            Frm_Main frm_Main = new Frm_Main();
            frm_Main.SaveConfig();
            Close();
        }

        private void SetColor(object sender)
        {
            Button btn = (Button)sender;
            ColorDialog colordialog = new ColorDialog();
            if (colordialog.ShowDialog() == DialogResult.OK)
            {
                btn.BackColor = colordialog.Color;
            }
            colordialog.Dispose();
        }

        private void Btn_Forecolor_Click(object sender, EventArgs e)
        {
            SetColor(Btn_Forecolor);
        }

        private void Btn_Backcolor_Click(object sender, EventArgs e)
        {
            SetColor(Btn_Backcolor);
        }
    }
}
