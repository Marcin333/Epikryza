using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplicationEpikryza
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'forHealthDBDataSet.tblDanePacjenta' table. You can move, or remove it, as needed.
            this.tblDanePacjentaTableAdapter.Fill(this.forHealthDBDataSet.tblDanePacjenta);

        }

        private void btnSzukaj_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(textSearchBox.Text)) && textSearchBox.Text.Length == 11)
            {
                var query = from d in this.forHealthDBDataSet.tblDanePacjenta
                            where d.Pesel == textSearchBox.Text
                            select d;

                var today = DateTime.Today;
                var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                string kobieta = "Kobieta";
                string men = "Mężczyzna";
                if (query.Any())
                {
                    foreach (var n in query)
                    {
                        var b = (n.Data_Urodzenia.Year * 100 + n.Data_Urodzenia.Month) * 100 + n.Data_Urodzenia.Day;
                        int age = (a - b) / 10000;

                        textBoxPacjent_ID.Text = n.ID_Pacjenta.ToString();
                        textDataRejestracji.Text = today.ToString("yyyy/MM/dd");
                        textNazwisko.Text = n.Nazwisko;
                        textImie.Text = n.Imie;
                        textBoxPesel.Text = n.Pesel;
                        textWiek.Text = n.Data_Urodzenia.ToString("yyyy/MM/dd");
                        textAge.Text = age.ToString();
                        textPlec.Text = (n.Plec.Contains("1")) ? kobieta : men;
                        textTel.Text = n.Tel_Komorkowy;
                        textEmail.Text = n.Email;
                        textMiasto.Text = n.Miasto_Zam;
                    }
                    textSearchBox.Text = String.Empty;
                }
                else
                {
                    MessageBox.Show("Nie znaleziono rekordu. \nSprawdź poprawność wpisanego numeru pesel.");
                }
               
            } else
            {
                MessageBox.Show("Nie znaleziono rekordu.");
                textSearchBox.Text = String.Empty;

            }
        }

        private void btnAlergen_Click(object sender, EventArgs e)
        {
            
        }

        private void bDodajAlergen_Click(object sender, EventArgs e)
        {
            Object selectedItem = comboBAlergen.SelectedItem;
            this.dataGridViewAlergen.Rows.Add(selectedItem);
            int counter;
            int alergenyTotal = 0;

            for (counter = 0; counter < (dataGridViewAlergen.Rows.Count);
               counter++)
            {
                if (dataGridViewAlergen.Rows[counter].Cells["alergen"].Value
                    != null)
                {
                    if (dataGridViewAlergen.Rows[counter].
                        Cells["alergen"].Value.ToString().Length != 0)
                    {
                        alergenyTotal += 1;
                        this.textBoxCountAlergeny.Text = alergenyTotal.ToString();
                        Console.WriteLine(alergenyTotal);
                    }
                }
            }
        }

        private void btnDodajViofo_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedItemMata = comboBoxMata.Text;
                string mata = selectedItemMata.Length > 0 ? selectedItemMata.ToString().Substring(10) : selectedItemMata;
                Object selectedItemPx = comboBoxPx.SelectedItem;
                Object selectedItemMx = comboBoxMx.SelectedItem;
                string natezenie = comboBoxNatezenie.Text;
                string krotnosc = numericUDKrotnosc.Value.ToString();
                string data = String.Format("{0}x {1}{2}{3}({4}) ", krotnosc, mata, selectedItemPx, 
                            selectedItemMx, natezenie);
                //myComboBox.Text.Clear();
                //this.dataGridViewViofor.Rows.Add();
                if (radioBtnRano.Checked)
                {
                    textBoxWRano.Text = data;
                }
                if (radioBtnPopoludniu.Checked)
                {
                    textBoxWPoPoludniu.Text = data;
                }
                if (radioBtnWieczor.Checked)
                {
                    textBoxWwieczor.Text = data;
                }
                
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void panelNiezyt_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(0, 0, 465, 76); //this.ClientRectangle.Height - 1
            Pen p = new Pen(Color.White, 2);
            e.Graphics.DrawRectangle(p, r);
        }

        private void radioBtnPrzewlekly_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Yellow;
            if (radioBtnNiezyt.Checked || radioBtnOstry.Checked)
            {
                ctrl.BackColor = SystemColors.GradientInactiveCaption;
            }
        }

        private void radioBtnNiezyt_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Orange;
            if (radioBtnPrzewlekly.Checked || radioBtnOstry.Checked)
            {
                ctrl.BackColor = SystemColors.GradientInactiveCaption;
            }
        }

        private void radioBtnOstry_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Red;
            if (radioBtnNiezyt.Checked || radioBtnPrzewlekly.Checked)
            {
                ctrl.BackColor = SystemColors.GradientInactiveCaption;
            }
        }

        private void radioBtnRano_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.MenuHighlight;
        }

        private void radioBtnPopoludniu_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.MenuHighlight;
        }

        private void radioBtnWieczor_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = SystemColors.MenuHighlight;
        }

        private void btnZapiszWszystko_Click(object sender, EventArgs e)
        {
            try
            {
                // Vioforoterapia
                string rano = textBoxWRano.Text;
                string poPoludniu = textBoxWPoPoludniu.Text;
                string wieczor = textBoxWwieczor.Text;
                //Console.WriteLine("+++Rano {0} popoludnie {1} wieczor {2}", rano, poPoludniu, wieczor);
                // Wywiad
                var wizytaPodKon = groupBoxWizyta.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
                string grypy = comboBoxGrypy.Text;
                string anginy = comboBoxAnginy.Text;
                var omdlenia = groupBoxOmdlenia.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
                var nieprzytomny = groupBoxNieprzytomny.Controls.OfType<RadioButton>()
                                        .FirstOrDefault(r => r.Checked);
                Object niezyty = comboBoxNiezyty.Text;
                var niezytRodzaj = groupBoxRodzaje.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
                //int counter;
                //int alergenyTotal = 0;
                
                foreach (DataGridViewRow row in dataGridViewSzpital.Rows)
                {
                    if (row.Cells["szpital"].Value != null && row.Cells["ileRazySzpital"].Value != null)
                    {
                        if (row.Cells["szpital"].Value.ToString().Length != 0 && row.Cells["ileRazySzpital"].Value.ToString().Length != 0)
                        {
                            Console.Write("+++ {0} {1}", row.Cells["szpital"].Value, row.Cells["ileRazySzpital"].Value);
                        }
                    }
                }
                  
                //string omdlenia = radioBtnOmdleniaTak.Checked ? radioBtnOmdleniaTak.Text : radioBtnOmdleniaNie.Text;
                //string niePrzytomny = radioBtnNieprzytTak.Checked ? radioBtnNieprzytTak.Text : radioBtnNieprzytNie.Text;

                //string nieprzytomnyTak = radioBtnNieprzytTak.Checked.ToString();
                //bool podstawowa = radioBtnPodstawowa.Checked; //? radioBtnPodstawowa.Text : null;
                //string kontrolna = radioBtnKontrolna.Checked ? radioBtnKontrolna.Text : null;
                //Console.WriteLine("++++ {0} {1}", podstawowa, kontrolna);
                //Note that this requires that all of the radio buttons be directly in the same container(eg, Panel or Form), and that there is only one group in the container. If that is not the case, you could make List< RadioButton > s in your constructor for each group, then write list.FirstOrDefault(r => r.Checked).


                Console.Write("+++ Niezyt rodzaj {0} omdlenia {1} nieprzytomny {2} wizyta Pod Kon {3}", niezytRodzaj.Text, omdlenia.Text, nieprzytomny.Text, wizytaPodKon.Text);
                //GetRadioBtnText(radioBtnOmdleniaTak);
                //object omdlenia = radiobtnom
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetRadioBtnText(RadioButton radioBtn)
        {
            if (radioBtn.Checked)
            {
                string radioBtnText = radioBtn.Text;
            }
        }

        private void DodajSzpital_Click(object sender, EventArgs e)
        {   
            if (!(String.IsNullOrEmpty(textBoxSzpital.Text)) && !(String.IsNullOrEmpty(comboBoxIleRazySzpital.Text)))
            {
                this.dataGridViewSzpital.Rows.Add(textBoxSzpital.Text, comboBoxIleRazySzpital.Text);
                textBoxSzpital.Text = String.Empty;
                comboBoxIleRazySzpital.Text = String.Empty;
            }
            
        }

        private void btnDodajBol_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(textBoxBole.Text)))
            {
                dataGridViewBole.Rows.Add(textBoxBole.Text, 
                    checkBoxSporadycznie.Checked ? checkBoxSporadycznie.Text : null,
                    checkBoxPrzewlekle.Checked ? checkBoxPrzewlekle.Text : null,
                    checkBoxOstre.Checked ? checkBoxOstre.Text : null);
            }
            textBoxBole.Text = String.Empty;
            checkBoxSporadycznie.Checked = false;
            checkBoxSporadycznie.BackColor = SystemColors.GradientInactiveCaption;
            checkBoxPrzewlekle.Checked = false;
            checkBoxPrzewlekle.BackColor = SystemColors.GradientInactiveCaption;
            checkBoxOstre.Checked = false;
            checkBoxOstre.BackColor = SystemColors.GradientInactiveCaption;
        }

        private void checkBoxSporadycznie_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Yellow;
        }

        private void checkBoxPrzewlekle_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Orange;
        }

        private void checkBoxOstre_CheckedChanged(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            ctrl.BackColor = Color.Red;
        }
    }
}
