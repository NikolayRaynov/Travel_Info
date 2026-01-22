# 🗺️ Travel_Info Application

A travel guide application where users can list visited places and save new ones. The system automatically organizes photos into specific category folders and includes a secure way to edit or delete personal comments.

---

## 🌟 Key Features

- **User Registration and Authentication**: Secure process for creating an account and logging into the system.
- **Adding Places**: Registered users can add new destinations they have visited or would like to visit.
- **Place Lists**:
  - **Places to Visit (Wishlist)**: Ability to add destinations to a "planning" list.
  - **Favorite Places**: Option to mark already visited places as "favorites."
- **Filtering by Categories**: Easily browse and filter places by predefined categories **(e.g., beaches, mountains, sea, strolls, historical landmarks, etc.)**.
- **Comments**:
  - Users can write comments under each visited place.
  - Ability to edit or delete a comment, but only if it belongs to the current user.
- **Image Gallery**: Each destination features a carousel that allows viewing of uploaded photos.
- **Categorized Image Storage**: Uploaded images are automatically organized into category-specific folders within **wwwroot/images/ (e.g., wwwroot/images/Category1/)**.
- **Destination Management**: Users can delete a destination, but only if they created it.

---

## 🛡️ Security Features

The project has been developed with a focus on security and includes protections against common web attacks:

- **SQL Injection**: Prevention of malicious SQL code injection.
- **Cross-Site Scripting (XSS)**: Protection against client-side script injection.
- **Cross-Site Request Forgery (CSRF)**: Measures to prevent unauthorized commands executed on behalf of the user.
- **Parameter Tampering**: Protection against manipulation of parameters in requests.
- **AntiForgeryToken**: Use of anti-forgery tokens for additional security in POST requests.

---

## 🛠️ Technologies Used

### Backend

- **ASP.NET Core**: Robust and scalable web framework.
- **Entity Framework 8**: Efficient database interactions with LINQ.
- **SQL Server**: Database system for storing application data.

### Frontend

- **Bootstrap 5**: Responsive and modern UI components.

### Testing

- **NUnit**: Comprehensive testing framework for unit tests.
- **Moq**: Mocking framework for creating mock objects in unit testing.
- **MockQueryable**: Simplifying LINQ mocking for Entity Framework queries during testing.

---

## 🚀 How to Run the Project

1.  Clone the repository:
    ```bash
    git clone https://github.com/NikolayRaynov/Travel_Info.git
    ```
2.  Navigate to the project directory:
    ```bash
    cd Travel_Info
    ```
3.  Restore dependencies:
    ```bash
    dotnet restore
    ```
4.  Run the application:
    ```bash
    dotnet run
    ```
5.  Open your browser and navigate to:
    ```
    http://localhost:7019
    ```

---

## 📜 License

This project is licensed under the MIT License. See the <a href="https://github.com/DefinitelyTyped/docs/blob/master/LICENSE-MIT">LICENSE</a> file for details.
