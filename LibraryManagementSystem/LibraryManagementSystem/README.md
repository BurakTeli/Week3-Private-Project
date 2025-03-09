# 📚 Library Management System

The **Library Management System** is an **ASP.NET Core MVC** application that allows users to manage books, authors, and authentication within a library system.

## 📂 Project Structure

The project is structured into various **Controllers**, each responsible for different functionalities. These controllers handle authentication, author management, book management, and the main pages of the system.

---

## 🏗 Controllers Overview

### 1️⃣ **AccountController** - User Authentication
The **AccountController** is responsible for handling user authentication, including login functionality.
- **`Login (GET)`** → Displays the login page.
- **`Login (POST)`** → Processes user login credentials.
  - If the email is `"burak@gmail.com"` and password is `"1234"`, the user is redirected to the home page.
  - If the credentials are invalid, an error message is displayed.

---

### 2️⃣ **AuthorController** - Author Management
The **AuthorController** manages the creation, updating, listing, and deletion of authors.
- **`List (GET)`** → Displays a list of all available authors.
- **`Create (GET, POST)`** → 
  - Shows a form to add a new author.
  - Processes the submission and adds the author to the system.
- **`Edit (GET, POST)`** →
  - Shows a form to edit an existing author’s details.
  - Updates the author’s information, including their name in all related books.
- **`Details (GET)`** → Displays the details of a specific author.
- **`Delete (DELETE)`** → 
  - Removes an author from the system.
  - Updates the books associated with the deleted author, setting the **AuthorId** to `0` and the name to `"Unknown"`.

---

### 3️⃣ **BookController** - Book Management
The **BookController** manages the creation, updating, listing, and deletion of books.
- **`List (GET)`** → Displays all available books.
- **`Create (GET, POST)`** →
  - Shows a form to add a new book.
  - Assigns the book to an author (if available) or marks it as `"Unknown Author"`.
- **`Edit (GET, POST)`** →
  - Shows a form to edit a book.
  - Updates book details and reassigns the book to a different author if necessary.
- **`Details (GET)`** → Displays book details.
- **`Delete (DELETE)`** →
  - Removes a book from the system.
  - Ensures that the book is also removed from the author's book list.

---

### 4️⃣ **HomeController** - Main Pages
The **HomeController** manages the main pages of the library system.
- **`Index (GET)`** → Displays the home page.
- **`About (GET)`** → Displays the about page.

---

## 🛠 Technologies Used

The following technologies were used to develop the project:

- **ASP.NET Core MVC** - Web framework for building the application.
- **C#** - Primary programming language.
- **Razor Views** - Used for dynamic content rendering.
- **Entity Framework Core (Not implemented yet, planned for future updates)** - ORM for database integration.
- **.NET 6+** - Framework version.

---

## 🚀 Setup & Run Instructions

To run the project on your local machine, follow these steps:

1. **Clone the repository**:
   ```sh
   git clone https://github.com/your_username/LibraryManagementSystem.git
