using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CourseBox3.Resources;

namespace CourseBox3
{
    public class TodoItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "major")]
        public string Major { get; set; }

        [JsonProperty(PropertyName = "grade")]
        public string Grade { get; set; }

        [JsonProperty(PropertyName = "coursename")]
        public string CourseName { get; set; }

        [JsonProperty(PropertyName = "coursetime")]
        public string CourseTime { get; set; }

        [JsonProperty(PropertyName = "courseplace")]
        public string CoursePlace { get; set; }

        // [JsonProperty(PropertyName = "info")]
        // public string Info { get; set; }
    }

    public partial class MainPage : PhoneApplicationPage
    {
        // MobileServiceCollectionView implements ICollectionView (useful for databinding to lists) and 
        // is integrated with your Mobile Service to make it easy to bind your data to the ListView
        private MobileServiceCollection<TodoItem, TodoItem> courses;

        private IMobileServiceTable<TodoItem> courseTable = App.MobileService.GetTable<TodoItem>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        /*
            private async void InsertTodoItem(TodoItem todoItem)
            {
                // This code inserts a new TodoItem into the database. When the operation completes
                // and Mobile Services has assigned an Id, the item is added to the CollectionView
                await todoTable.InsertAsync(todoItem);
                items.Add(todoItem);
            }

            private async void RefreshTodoItems()
            {
                // This code refreshes the entries in the list view be querying the TodoItems table.
                // The query excludes completed TodoItems
                try
                {
                    items = await todoTable
                        .Where(todoItem => todoItem.Complete == false)
                        .ToCollectionAsync();
                }
                catch (MobileServiceInvalidOperationException e)
                {
                    MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
                }

                ListItems.ItemsSource = items;
            }
    */
        private async void searchTodoItem(string text1, string text2)
        {
            // This code refreshes the entries in the list view be querying the TodoItems table.
            // The query excludes completed TodoItems
            try
            {
                courses = await courseTable
                    .Where(courseitem => courseitem.Major == text1 && courseitem.Grade == text2)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
            }


            idlist.ItemsSource = courses;
            namelist.ItemsSource = courses;
            timelist.ItemsSource = courses;
            placelist.ItemsSource = courses;
            //ListItems2.ItemsSource = items;
        }

        private async void searchoneTodoItem(string text1, string text2, string text3)
        {
            // This code refreshes the entries in the list view be querying the TodoItems table.
            // The query excludes completed TodoItems
            try
            {
                courses = await courseTable
                    .Where(courseitem => courseitem.Major == text1 && courseitem.Grade == text2 && courseitem.CourseName == text3)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
            }


            idlist.ItemsSource = courses;
            namelist.ItemsSource = courses;
            timelist.ItemsSource = courses;
            placelist.ItemsSource = courses;
            //ListItems2.ItemsSource = items;
        }


        /*       private async void UpdateCheckedTodoItem(TodoItem item)
               {
                   // This code takes a freshly completed TodoItem and updates the database. When the MobileService 
                   // responds, the item is removed from the list 
                   await todoTable.UpdateAsync(item);
                   items.Remove(item);
               }

               private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
               {
                   RefreshTodoItems();
               }

               private void ButtonSave_Click(object sender, RoutedEventArgs e)
               {
                   var todoItem = new TodoItem { Text = TodoInput.Text };
                   InsertTodoItem(todoItem);
               }

              private void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
               {
                   CheckBox cb = (CheckBox)sender;
                   TodoItem item = cb.DataContext as TodoItem;
                   item.Complete = true;
                   UpdateCheckedTodoItem(item);
               }
		
               protected override void OnNavigatedTo(NavigationEventArgs e)
               {
                   RefreshTodoItems();
               }
       */
        private void search_clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
            string major_text = major_search.Text;
            string grade_text = grade_search.Text;
            string course_text = course_search.Text;
           // Console.Write(111111111);
            if (major_text.Length != 0 && grade_text.Length != 0 && course_text.Length == 0)
            {
                searchTodoItem(major_text, grade_text);
            }
            else if (major_text.Length != 0 && grade_text.Length != 0 && course_text.Length != 0)
            {
                searchoneTodoItem(major_text, grade_text, course_text);
            }


        }
    }
}