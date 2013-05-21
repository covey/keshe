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
using CourseBox_beta.Resources;

namespace CourseBox_beta
{
    public class TodoItem
    {
        public int Id { get; set; }

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

        [JsonProperty(PropertyName = "teacher")]
        public string Teacher { get; set; }

        [JsonProperty(PropertyName = "remarks")]
        public string Remarks { get; set; }


    }

    public partial class MainPage : PhoneApplicationPage
    {
        // MobileServiceCollectionView implements ICollectionView (useful for databinding to lists) and 
        // is integrated with your Mobile Service to make it easy to bind your data to the ListView
        private MobileServiceCollection<TodoItem, TodoItem> items;

        private IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<TodoItem>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private async void searchTodoItem(string text1, string text2)
        {
            // This code refreshes the entries in the list view be querying the TodoItems table.
            // The query excludes completed TodoItems
            try
            {
                items = await todoTable
                    .Where(courseitem => courseitem.Major == text1 && courseitem.Grade == text2)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
            }
            //     id.Visibility = Visibility;
            //      coursename.Visibility = Visibility;
            //     coursetime.Visibility = Visibility;
            //    courseplace.Visibility = Visibility;


            namelist.ItemsSource = items;
            timelist.ItemsSource = items;
            placelist.ItemsSource = items;
            teacherlist.ItemsSource = items;
            remarkslist.ItemsSource = items;

            //   NavigationService.Navigate(new Uri("/searchpage.xaml？msg="+courses, UriKind.Relative));

            //ListItems2.ItemsSource = items;
        }


        private async void searchoneTodoItem(string text1, string text2, string text3)
        {
            // This code refreshes the entries in the list view be querying the TodoItems table.
            // The query excludes completed TodoItems
            try
            {
                items = await todoTable
                    .Where(courseitem => courseitem.Major == text1 && courseitem.Grade == text2 && courseitem.CourseName == text3)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
            }

            // id.Visibility = Visibility;
            //   coursename.Visibility = Visibility;
            //   coursetime.Visibility = Visibility;
            //   courseplace.Visibility = Visibility;

            //    var item = new TodoItem(courses.ElementAt(0));

            namelist.ItemsSource = items;
            timelist.ItemsSource = items;
            placelist.ItemsSource = items;
            teacherlist.ItemsSource = items;
            remarkslist.ItemsSource = null;
            remarkslist1.ItemsSource = items;

          //  add_remarks.Visibility = Visibility;
        //    addremarks.Visibility = Visibility;
        }

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
        private async void UpdateRemarksTodoItem(TodoItem item)
        {
            // This code takes a freshly completed TodoItem and updates the database. When the MobileService 
            // responds, the item is removed from the list 
            await todoTable.UpdateAsync(item);
            searchoneTodoItem(major_search.Text, grade_search.Text, course_search.Text);

            // items.Remove(item);
        }
        private void addremarks_clicked(object sender, RoutedEventArgs e)
        {
            string remarks_text = TodoInput1.Text;

            CheckBox cb = (CheckBox)sender;
            TodoItem item = cb.DataContext as TodoItem;
            item.Remarks = remarks_text;
            UpdateRemarksTodoItem(item);
        }
    }
}