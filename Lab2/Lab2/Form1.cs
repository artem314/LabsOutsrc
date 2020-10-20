using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            string currentTable = "";
            if (Tables.SelectedIndex>=0)
            currentTable = Tables.SelectedItem.ToString();
            int columnID = Columns.SelectedIndex;
            string currentColumn = "";

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

                    string columnsToInsert = " (";
                    string columnsDefValues = " (";

                    foreach (object item in Columns.Items)
                    {
                        columnsToInsert += item.ToString() + ",";
                        columnsDefValues += "\'" + item.ToString() + "\'" + ",";
                    }
                    columnsToInsert += ") ";
                    columnsDefValues += ") ";

                    finalQuery = "INSERT INTO " + currentTable + columnsToInsert + "VALUES" + columnsDefValues + ";";
                    break;
                case 4:
                    string deleteCondition = "";
                    if (columnID >= 0)
                        deleteCondition = " WHERE " + currentColumn + " = condition";
                    finalQuery = "DELETE FROM " + currentTable + deleteCondition;
                    break;
                case 5:

                    string setCondtion = "";
                    string whereCondtion = "";
                    if (columnID >= 0)
                    {
                        whereCondtion = $"WHERE {currentColumn} = '{currentColumn}'";
                        setCondtion = $"{currentColumn} = '{currentColumn}'";
                    }
                    else
                    {
                        whereCondtion = "WHERE 'condition'";
                        foreach (object item in Columns.Items)
                        {
                            setCondtion += $"{item.ToString()} = '{item.ToString()}', ";
                        }
                    }
                    finalQuery = $"UPDATE {currentTable} SET {setCondtion} {whereCondtion}";
                    break;
            }

            textBox1.Text = finalQuery;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
