using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Practice_5
{
    public partial class Form1 : Form
    {
        BindingSource bs = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeData();
            InitializePictureBox();
            InitializePropertyGrid();
            InitializeChart();
            InitializeNavigatorBinding();
        }

        private void InitializeDataGridView()
        {
            dataGrid.AutoGenerateColumns = false;

            var cityNameColumn = new DataGridViewTextBoxColumn();
            cityNameColumn.DataPropertyName = "CityName";
            cityNameColumn.HeaderText = "Город";
            cityNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGrid.Columns.Add(cityNameColumn);

            var universityNameColumn = new DataGridViewTextBoxColumn();
            universityNameColumn.DataPropertyName = "UniversityName";
            universityNameColumn.HeaderText = "Название университета";
            universityNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGrid.Columns.Add(universityNameColumn);

            var establishedYearColumn = new DataGridViewTextBoxColumn();
            establishedYearColumn.DataPropertyName = "EstablishedYear";
            establishedYearColumn.HeaderText = "Год основания";
            establishedYearColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGrid.Columns.Add(establishedYearColumn);

            var numberOfStudentsColumn = new DataGridViewTextBoxColumn();
            numberOfStudentsColumn.DataPropertyName = "NumberOfStudents";
            numberOfStudentsColumn.HeaderText = "Количество студентов";
            numberOfStudentsColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGrid.Columns.Add(numberOfStudentsColumn);

            var typeOfUniversityColumn = new DataGridViewComboBoxColumn();
            typeOfUniversityColumn.DataPropertyName = "TypeOfUniversity";
            typeOfUniversityColumn.HeaderText = "Тип университета";
            typeOfUniversityColumn.DataSource = Enum.GetValues(typeof(eType));
            typeOfUniversityColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGrid.Columns.Add(typeOfUniversityColumn);

        }

        private void InitializeData()
        {
            var _universities = new List<University>
            {
                new University("Санкт-Петербург", "СПбПУ", 1899, "../../Images/spbpu.jpg", 32813, eType.Технический),
                new University("Санкт-Петербург", "СПбГПМУ", 1925, "../../Images/spbgpmu.jpg", 6149, eType.Медицинский),
                new University("Москва", "МФТИ", 1946, "../../Images/mftu.png", 6800, eType.Технический),
                new University("Москва", "РГГУ", 1991, "../../Images/rggu.jpg", 16800, eType.Гуманитарный),
                new University("Казань", "КГК", 1945, "../../Images/kk.png", 6745, eType.Творческий),
            };

            bs.DataSource = _universities;
            dataGrid.DataSource = bs;
        }

        private void InitializePictureBox()
        {
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.DataBindings.Add("ImageLocation", bs, "Logo", true);
            pictureBox.DoubleClick += pictureBox_Click;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "Картинка в формате jpg|*.jpg|Картинка в формате jpeg|*.jpeg"
            };
            if (opf.ShowDialog() == DialogResult.OK)
            {
                (bs.Current as University).Logo = opf.FileName;
                bs.ResetBindings(false);
            }
        }

        private void InitializePropertyGrid()
        {
            propertyGrid.DataBindings.Add("SelectedObject", bs, "");
        }

        private void InitializeChart()
        {
            chart.DataSource = from u in bs.DataSource as List<University>
                               group u by u.StrType into g
                               select new { Type = g.Key, Avg = g.Average(u => u.NumberOfStudents) };
            // по оси Х - значения свойства Type
            chart.Series[0].XValueMember = "Type";
            // по оси Y - значения свойства Avg
            chart.Series[0].YValueMembers = "Avg";
            // Убираем легенду, добавляем заголовок
            chart.Legends.Clear();
            chart.Titles.Add("Информация об университетах");

            //В случае изменений обновляем гистограмму
            bs.CurrentChanged += (o, e) => chart.DataBind();
        }

        private void InitializeNavigatorBinding()
        {
            bindingNavigator.BindingSource = bs;

            //Задание значений из DataGridView для выбора поля 
            foreach (DataGridViewColumn column in dataGrid.Columns)
                navComboBox.Items.Add(column.HeaderText);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.InitialDirectory = System.Environment.CurrentDirectory;
            sfd.Filter = "Файл в bin|*.bin|Файл в xml|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var isBinaryFormat = sfd.FilterIndex == 1 ? true : false;
                SerializeAndSave(sfd.FileName, isBinaryFormat);
            }
        }

        private void SerializeAndSave(string file, bool isBinaryFormat)
        {
            using (var stream = new FileStream(file, FileMode.Create))
            {
                if (isBinaryFormat)
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, bs.DataSource);
                }
                else
                {
                    var ser = new XmlSerializer(typeof(List<University>));
                    ser.Serialize(stream, bs.DataSource);
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.Environment.CurrentDirectory;
            ofd.Filter = "Файл в bin|*.bin|Файл в xml|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var isBinaryFormat = ofd.FilterIndex == 1 ? true : false;
                DeserializeAndLoad(ofd, isBinaryFormat);
            }
        }

        private void DeserializeAndLoad(OpenFileDialog ofd, bool isBinaryFormat)
        {
            using (var stream = new FileStream(ofd.FileName, FileMode.Open))
            {
                if (isBinaryFormat)
                {
                    var formatter = new BinaryFormatter();
                    bs.DataSource = (List<University>)formatter.Deserialize(stream);
                    
                }
                else
                {
                    var ser = new XmlSerializer(typeof(List<University>));
                    try
                    {
                        bs.DataSource = (List<University>)ser.Deserialize(stream);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Файл содержит ошибку");
                    }
                    
                }
            }
        }

        private void toolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(toolStripTextBox.Text) && navComboBox.SelectedItem != null)
            {
                // Получаем выбранное значение из ToolStripComboBox.
                var selectedValue = navComboBox.SelectedItem.ToString();

                // Получаем значение из ToolStripTextBox.
                var searchValue = toolStripTextBox.Text;

                // Очищаем выделение строк в DataGridView.
                dataGrid.ClearSelection();

                // Ищем столбец DataGridView, у которого название равно выбранному значению ToolStripComboBox.
                foreach (DataGridViewColumn column in dataGrid.Columns)
                {
                    if (column.HeaderText == selectedValue)
                    {
                        // Выделяем строки в DataGridView, у которых значение выбранного столбца равно значению из ToolStripTextBox.
                        foreach (DataGridViewRow row in dataGrid.Rows)
                        {
                            if(row.Cells[column.Index].Value != null)
                            {
                                //Если выбрано "Количество студентов", то отбираются только значения больше 
                                if (selectedValue == "Количество студентов")
                                {
                                    if (row.Cells[column.Index].Value != null && int.Parse(row.Cells[column.Index].Value.ToString()) >= int.Parse(searchValue))
                                        row.Visible = true;
                                    else
                                    {
                                        dataGrid.CurrentCell = null;
                                        row.Visible = false;
                                    }
                                }
                                else
                                {
                                    if (row.Cells[column.Index].Value.ToString().Contains(searchValue))
                                        row.Visible = true;
                                    else
                                    {
                                        dataGrid.CurrentCell = null;
                                        row.Visible = false;
                                    }
                                }
                            }
                        }

                        break;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGrid.Rows)
                    row.Visible = true;
            }
        }

    }

}
