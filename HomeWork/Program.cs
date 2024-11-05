using System;
using System.Text;

public class Node
{
    public object Data { get; set; }
    public Node Next { get; set; }

    public Node(object data)
    {
        Data = data;
        Next = null;
    }
}

public class LinkedList
{
    private Node head;
    private Node tail;
    public int Count { get; private set; }

    public LinkedList()
    {
        head = null;
        tail = null;
        Count = 0;
    }

    public void AddFirst(object data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            newNode.Next = head;
            head = newNode;
        }
        Count++;
    }

    public void AddLast(object data)
    {
        Node newNode = new Node(data);
        if (tail == null)
        {
            head = tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            tail = newNode;
        }
        Count++;
    }

    public void AddAt(int index, object data)
    {
        if (index < 0 || index > Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }

        if (index == 0)
        {
            AddFirst(data);
            return;
        }

        if (index == Count)
        {
            AddLast(data);
            return;
        }

        Node newNode = new Node(data);
        Node current = head;

        for (int i = 0; i < index - 1; i++)
        {
            current = current.Next;
        }

        newNode.Next = current.Next;
        current.Next = newNode;
        Count++;
    }

    public void RemoveFirst()
    {
        if (head == null) throw new InvalidOperationException("List is empty.");

        head = head.Next;
        if (head == null) tail = null;
        Count--;
    }

    public void RemoveLast()
    {
        if (head == null) throw new InvalidOperationException("List is empty.");

        if (head == tail)
        {
            head = tail = null;
        }
        else
        {
            Node current = head;
            while (current.Next != tail)
            {
                current = current.Next;
            }
            current.Next = null;
            tail = current;
        }
        Count--;
    }

    public void PrintList()
    {
        if (head == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        Node current = head;
        Console.WriteLine("List contents:");
        while (current != null)
        {
            PrintData(current.Data);
            current = current.Next;
        }
        Console.WriteLine("null");
    }

    private void PrintData(object data)
    {
        switch (data)
        {
            case string text:
                Console.WriteLine("String: " + text);
                break;
            case int number:
                Console.WriteLine("Integer: " + number);
                break;
            case byte[] imageData:
                Console.WriteLine("Image data: " + imageData.Length + " bytes");
                break;
            default:
                Console.WriteLine("Other type: " + data);
                break;
        }
    }
}

class Program
{
    static void Main()
    {
        var list = new LinkedList();

        while (true)
        {
            try
            {
                Console.WriteLine("\nChoose an operation:");
                Console.WriteLine("1. Add element at the beginning");
                Console.WriteLine("2. Add element at the end");
                Console.WriteLine("3. Add element at a specific position");
                Console.WriteLine("4. Remove the first element");
                Console.WriteLine("5. Remove the last element");
                Console.WriteLine("6. Print the list");
                Console.WriteLine("7. Exit the program");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddElement(list, true);
                        break;
                    case 2:
                        AddElement(list, false);
                        break;
                    case 3:
                        InsertAtPosition(list);
                        break;
                    case 4:
                        list.RemoveFirst();
                        Console.WriteLine("First element removed.");
                        break;
                    case 5:
                        list.RemoveLast();
                        Console.WriteLine("Last element removed.");
                        break;
                    case 6:
                        list.PrintList();
                        break;
                    case 7:
                        Console.WriteLine("Exiting program.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter a number.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }

    static void AddElement(LinkedList list, bool atBeginning)
    {
        Console.Write("Enter the value to add (type 'image' to simulate adding an image): ");
        string input = Console.ReadLine();

        object data;
        if (input.ToLower() == "image")
        {
            // Simulate adding an image as a byte array
            data = new byte[] { 1, 2, 3, 4 };  // Example image data (could be any byte array)
            Console.WriteLine("Simulated image data added.");
        }
        else if (int.TryParse(input, out int intResult))
        {
            data = intResult;
        }
        else
        {
            data = input;
        }

        if (atBeginning)
            list.AddFirst(data);
        else
            list.AddLast(data);

        Console.WriteLine("Element added successfully.");
    }

    static void InsertAtPosition(LinkedList list)
    {
        Console.Write("Enter the position to insert at: ");
        int position = int.Parse(Console.ReadLine());

        Console.Write("Enter the value to insert (type 'image' to simulate adding an image): ");
        string input = Console.ReadLine();

        object data;
        if (input.ToLower() == "image")
        {
            data = new byte[] { 1, 2, 3, 4 }; 
            Console.WriteLine("Simulated image data added.");
        }
        else if (int.TryParse(input, out int intResult))
        {
            data = intResult;
        }
        else
        {
            data = input;
        }

        list.AddAt(position, data);
        Console.WriteLine("Element inserted at position " + position + " successfully.");
    }
}
