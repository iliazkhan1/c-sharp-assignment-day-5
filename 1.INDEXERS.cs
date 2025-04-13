using System;
namespace SimpleLibrarySystem
{
    class Book
    {
        public string Title;
        public string Author;
        public int Year;

        public Book(string title, string author, int year)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || year <= 0)
            {
                throw new ArgumentException("Invalid book details provided.");
            }

            Title = title;
            Author = author;
            Year = year;
        }

        public void Display()
        {
            Console.WriteLine($"Title: {Title}, Author: {Author}, Year: {Year}");
        }
    }



    class Library
    {
        private Book[] books = new Book[100];
        private int count = 0;

        public Book this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                {
                    Console.WriteLine("Invalid index.");
                    return null;
                }
                return books[index];
            }
            set
            {
                if (index < 0 || index >= count)
                {
                    Console.WriteLine("Invalid index.");
                    return;
                }
                books[index] = value;
            }
        }

        public Book this[string title]
        {
            get
            {
                for (int i = 0; i < count; i++)
                {
                    if (books[i].Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    {
                        return books[i];
                    }
                }
                return null;
            }
        }

        public void AddBook(Book book)
        {
            if (count >= books.Length)
            {
                Console.WriteLine("Library is full.");
                return;
            }

            books[count] = book;
            count++;
            Console.WriteLine("Book added successfully.");
        }

        public void RemoveBook(int index)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Invalid index. Cannot remove.");
                return;
            }

            for (int i = index; i < count - 1; i++)
            {
                books[i] = books[i + 1];
            }

            books[count - 1] = null;
            count--;
            Console.WriteLine("Book removed successfully.");
        }

        public void ShowBooks()
        {
            if (count == 0)
            {
                Console.WriteLine("Library is empty.");
                return;
            }

            Console.WriteLine("\nBooks in the Library:");
            for (int i = 0; i < count; i++)
            {
                Console.Write($"{i}. ");
                books[i].Display();
            }
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Library Menu ---");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Show All Books");
                Console.WriteLine("3. Get Book by Index");
                Console.WriteLine("4. Search Book by Title");
                Console.WriteLine("5. Remove Book by Index");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice (1-6): ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        try
                        {
                            Console.Write("Enter Title: ");
                            string title = Console.ReadLine();

                            Console.Write("Enter Author: ");
                            string author = Console.ReadLine();

                            Console.Write("Enter Year: ");
                            string yearStr = Console.ReadLine();
                            int year;

                            if (!int.TryParse(yearStr, out year) || year <= 0)
                            {
                                Console.WriteLine("Invalid year.");
                                break;
                            }

                            Book newBook = new Book(title, author, year);
                            library.AddBook(newBook);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        break;

                    case "2":
                        library.ShowBooks();
                        break;

                    case "3":
                        Console.Write("Enter index to get book: ");
                        string indexStr = Console.ReadLine();
                        int index;

                        if (!int.TryParse(indexStr, out index))
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            break;
                        }

                        Book bookByIndex = library[index];
                        if (bookByIndex != null)
                        {
                            bookByIndex.Display();
                        }
                        break;

                    case "4":
                        Console.Write("Enter title to search: ");
                        string searchTitle = Console.ReadLine();
                        Book foundBook = library[searchTitle];
                        if (foundBook != null)
                            foundBook.Display();
                        else
                            Console.WriteLine("Book not found.");
                        break;

                    case "5":
                        Console.Write("Enter index to remove book: ");
                        string removeIndexStr = Console.ReadLine();
                        int removeIndex;

                        if (!int.TryParse(removeIndexStr, out removeIndex))
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            break;
                        }

                        library.RemoveBook(removeIndex);
                        break;

                    case "6":
                        exit = true;
                        Console.WriteLine("Exiting Library System...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 6.");
                        break;
                }
            }
        }
    }
}
