using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Queries.Items.Add("Создание БД");
            Queries.Items.Add("Создание Таблицы");
            Queries.Items.Add("Выборка");
            Queries.Items.Add("Добавление");
            Queries.Items.Add("Удаление");
            Queries.Items.Add("Модификация");

            databaseAdapter.GetInstance();
            Tables.Items.AddRange(databaseAdapter.getTableNames());
        }

        private void Tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentTable = Tables.SelectedItem.ToString();
            Columns.Items.Clear();
            Columns.Items.AddRange(databaseAdapter.getColumnNames(currentTable));

        }

        private void button1_Click(object sender, EventArgs e)
        {

            int currentQueryType = Queries.SelectedIndex;
            string finalQuery = "";
            string currentTable = "table_name";

            if (Tables.SelectedIndex >= 0)
            {
                currentTable = Tables.SelectedItem.ToString();
            }

            int columnID = Columns.SelectedIndex;

            string currentColumn = "column_name";
            if (columnID >= 0)
            {
                currentColumn = Columns.SelectedItem.ToString();
            }

            switch (currentQueryType)
            {
                case 0:
                    finalQuery = "CREATE DATABASE db_name;";
                    break;
                case 1:
                    finalQuery = "CREATE TABLE table_name(\n" +
                        "ID INT AUTO_INCREMENT\n" +
                        ")";
                    break;
                case 2:
                    string cols = "*";
                    if (columnID >= 0)
                        cols = Columns.SelectedItem.ToString();
                    finalQuery = $"SELECT {cols} FROM {currentTable}";
                    break;
                case 3:
                    string columnsDefValues = "";

                    foreach (object item in Columns.Items)
                    {
                        columnsDefValues += $"'{item.ToString()}' ,";
                    }

                    finalQuery = $"INSERT INTO {currentTable} \n" +
                        $"VALUES ({columnsDefValues});";
                    break;
                case 4:
                    string deleteCondition = "";

                    if (columnID >= 0)
                        deleteCondition = $"WHERE {currentColumn} = condition";
                    finalQuery = $"DELETE FROM {currentTable} {deleteCondition};";

                    break;
                case 5:

                    string setCondtion = "";
                    string whereCondtion = "";
                    if (columnID >= 0)
                    {
                        whereCondtion = "WHERE 'condition'";
                        //whereCondtion = $"WHERE {currentColumn} = '{currentColumn}'";
                        setCondtion = $"{currentColumn} = '{currentColumn}'";
                    }
                    else
                    {

                        foreach (object item in Columns.Items)
                        {
                            setCondtion += $"{item.ToString()} = '{item.ToString()}',";
                        }
                    }
                    finalQuery = $"UPDATE {currentTable} SET {setCondtion} {whereCondtion};";
                    break;
            }

            textBox1.Text = finalQuery;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                saveQuery();
            }
        }

        private void saveQuery()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "SQL Files (*.sql)|*.sql|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.FileName = "SQLquery";
                saveFileDialog.Title = "Сохранить запрос";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string queryToSave = textBox1.Text;
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(queryToSave);
                        MessageBox.Show("Запрос сохранен");
                    }
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "SQL Files (*.sql)|*.sql|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    textBox1.Text = fileContent;
                }
            }
        }

        private void onClose()
        {
            const string message = "Вы хотите сохранить запрос?";
            const string caption = "Сохранить запрос?";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                saveQuery();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                onClose();
            }
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                onClose();
            }
            this.Close();
        }
    }
}
