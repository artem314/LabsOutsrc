using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab1
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        private void editCategoriesList()
        {
            using (StreamWriter fs = File.CreateText("Categories.json"))
            {
                string json = JsonConvert.SerializeObject(Categories);
                fs.Write(json);
            }
        }

        private notebookDBEntities1 db;

        private string _statusString { get; set; }

        public string StatusString
        {
            get { return _statusString; }
            set
            {
                _statusString = value;
                OnPropertyChanged("StatusString");
            }
        }


        private IEnumerable<Contacts> backupContactsList { get; set; }

        private string _searchString { get; set; }

        public string searchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;

                if (searchString == "")
                {
                    ContactsList = backupContactsList;
                }

                if (searchString != "")
                {
                    ContactsList = backupContactsList.Where(x =>
                   (x.FIO.IndexOf(searchString) != -1)
                    || (x.mobile.IndexOf(searchString) != -1)
                    || (x.phone.IndexOf(searchString) != -1)
                    || (x.email.IndexOf(searchString) != -1)
                    || (x.category.IndexOf(searchString) != -1)
                    || (x.address.IndexOf(searchString) != -1)
                    || (x.notes.IndexOf(searchString) != -1)
                    );
                }

                OnPropertyChanged("searchString");
            }
        }

        private Contacts _selectedContact { get; set; }
        public Contacts SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;

                if (SelectedContact != null)
                {
                    selectedCategory = SelectedContact.category;
                }

                OnPropertyChanged("SelectedContact");
            }
        }

        private string _textCategory { get; set; }
        public string textCategory
        {
            get { return _textCategory; }
            set
            {
                _textCategory = value;
                OnPropertyChanged("textCategory");
            }
        }

        private string _selectedCategory { get; set; }
        public string selectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("selectedCategory");
            }
        }

        private int _indexCategory { get; set; }
        public int indexCategory
        {
            get { return _indexCategory; }
            set
            {

                if (indexCategory != -1 && value != -1)
                {
                    _indexCategory = value;
                    OnPropertyChanged("indexCategory");
                }
            }
        }

        private ObservableCollection<string> _categories { get; set; }
        public ObservableCollection<string> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        private RelayCommand _clearInputCommand { get; set; }

        public RelayCommand clearInputCommand
        {
            get
            {
                return _clearInputCommand ??
                  (_clearInputCommand = new RelayCommand(obj =>
                  {
                      SelectedContact = new Contacts();
                  }));
            }
        }

        private RelayCommand _addContactCommand { get; set; }

        public RelayCommand addContactCommand
        {
            get
            {
                return _addContactCommand ??
                  (_addContactCommand = new RelayCommand(obj =>
                  {
                      Contacts contact = new Contacts();
                      contact = SelectedContact;

                      if (String.IsNullOrEmpty(contact.FIO))
                      {
                          MessageBox.Show("Необходимо ввести Контактные данные");
                          return;
                      }

                      if (String.IsNullOrEmpty(contact.email)
                      && String.IsNullOrEmpty(contact.phone)
                      && String.IsNullOrEmpty(contact.mobile)
                      && String.IsNullOrEmpty(contact.address))
                      {
                          MessageBox.Show("Все поля, касающиеся телефона, адреса или электронного адреса не могут быть пустыми одновременно");
                          return;
                      }

                      try
                      {
                          if (selectedCategory != null)
                          {
                              contact.category = selectedCategory;
                          }
                          db.Contacts.Add(contact);
                          db.SaveChanges();

                          db.Contacts.Load();
                          ContactsList = db.Contacts.Local.ToBindingList();

                          StatusString = "Добавление успешно";

                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand _deleteCategoryCommand;
        public RelayCommand deleteCategoryCommand
        {
            get
            {
                return _deleteCategoryCommand ??
                  (_deleteCategoryCommand = new RelayCommand(obj =>
                  {
                      if (selectedCategory != null)
                      {
                          try
                          {
                              Categories.Remove(selectedCategory);
                              StatusString = "Удаление категории успешно";
                              saveCategories();
                              editCategoriesList();
                          }
                          catch (Exception ex)
                          {
                              MessageBox.Show(ex.Message);
                          }
                      }
                  }));
            }
        }

        private void saveCategories()
        {

        }


        private RelayCommand _editCategoryCommand;
        public RelayCommand editCategoryCommand
        {
            get
            {
                return _editCategoryCommand ??
                  (_editCategoryCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          int i = indexCategory;
                          if (i != -1)
                          {

                              Categories[i] = textCategory;
                              StatusString = "Изменение категории успешно";
                              saveCategories();
                              editCategoriesList();
                          }
                          else
                          {
                              MessageBox.Show("Выберите Категорию");
                          }
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }

                  }));
            }
        }

        private RelayCommand _addCategoryCommand;
        public RelayCommand addCategoryCommand
        {
            get
            {
                return _addCategoryCommand ??
                  (_addCategoryCommand = new RelayCommand(obj =>
                  {
                      Categories.Add(textCategory);
                      StatusString = "Добавление успешно";
                      saveCategories();
                      editCategoriesList();
                  }));
            }
        }

        private RelayCommand _editContactCommand;
        public RelayCommand editContactCommand
        {
            get
            {
                return _editContactCommand ??
                  (_editContactCommand = new RelayCommand(obj =>
                  {

                      if (SelectedContact != null)
                      {
                          Contacts contact = db.Contacts.Find(SelectedContact.Id);
                          if (contact != null)
                          {
                              contact = SelectedContact;
                              contact.category = selectedCategory;
                              try
                              {
                                  db.Entry(contact).State = EntityState.Modified;
                                  db.SaveChanges();
                                  StatusString = "Редактирование успешно";
                              }
                              catch (Exception ex)
                              {
                                  MessageBox.Show(ex.Message);
                              }
                          }
                          else
                          {
                              MessageBox.Show("Элемент не найден в базе");
                          }
                      }
                      else
                      {
                          MessageBox.Show("Выберите элемент для редактирования");
                      }

                  }));
            }
        }

        private RelayCommand _deleteContactCommand;
        public RelayCommand deleteContactCommand
        {
            get
            {
                return _deleteContactCommand ??
                  (_deleteContactCommand = new RelayCommand(SelectedContact =>
                  {

                      if (SelectedContact != null)
                      {
                          Contacts contact = SelectedContact as Contacts;
                          try
                          {
                              db.Contacts.Remove(contact);
                              db.SaveChanges();
                              StatusString = "Удаление успешно";

                          }
                          catch (Exception ex)
                          {
                              MessageBox.Show(ex.Message);
                          }
                      }
                      else
                      {
                          MessageBox.Show("Выберите элемент для удаление");
                      }
                  }));
            }
        }

        private IEnumerable<Contacts> _contactsList { get; set; }

        public IEnumerable<Contacts> ContactsList
        {
            get { return _contactsList; }
            set
            {
                _contactsList = value;

                OnPropertyChanged("ContactsList");
            }
        }


        public ApplicationViewModel()
        {
            try
            {
                SelectedContact = new Contacts();

                StatusString = "";
                //не менять!
                string appPath = System.AppDomain.CurrentDomain.BaseDirectory;
                string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={appPath}notebookDB.mdf;Integrated Security=True;Connect Timeout=30";
                //не менять!
                try
                {
                    db = new notebookDBEntities1();
                    db.Database.Connection.ConnectionString = connectionString;
                    db.Contacts.Load();
                    ContactsList = db.Contacts.Local.ToBindingList();
                    backupContactsList = new ObservableCollection<Contacts>(ContactsList);
                    Categories = new ObservableCollection<string>();
                    //не менять!
                    Categories = JsonConvert.DeserializeObject<ObservableCollection<string>>(File.ReadAllText(appPath+ "Categories.json"));
                   
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
