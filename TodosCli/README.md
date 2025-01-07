
# TODO Management Application

This is a simple TODO management application that interacts with a mock API to manage tasks. It allows users to view, add, update, and delete TODO items. The app uses a mock API to handle all the requests for the CRUD operations.

## Features
- **View TODOs**: Display all available TODOs or show a message if no TODOs are found.
- **Add TODO**: Allow the user to add a new TODO by providing the name, importance, description, and deadline.
- **Update TODO**: Modify an existing TODO by choosing a field to update and providing a new value.
- **Delete TODO**: Delete a TODO by selecting it from the list.

## Operations
1. **View TODOs**: Displays all the details of the selected TODO.
2. **Add TODO**: Prompts the user to input the details of a new TODO and sends a POST request to the server.
3. **Update TODO**: Displays the details of the TODO and allows the user to change a specific field. A PATCH request is sent to update the record.
4. **Delete TODO**: Deletes a selected TODO by sending a DELETE request to the API.

## API Usage
This application interacts with a **Mock API** that provides a simple RESTful interface for managing TODOs. The following endpoints are used:

- **GET /api/v1/todos**: Fetches the list of all TODOs.
- **POST /api/v1/todos**: Adds a new TODO to the system.
- **PATCH /api/v1/todos/:id**: Updates a TODO based on the provided ID.
- **DELETE /api/v1/todos/:id**: Deletes a TODO based on the provided ID.

## Requirements
- A C# compatible environment to run the application.
- An active internet connection to interact with the mock API.

## Installation
1. Clone this repository to your local machine.
2. Open the project in your preferred IDE (e.g., Visual Studio or Rider).
3. Ensure you have the necessary dependencies and configurations set up to run C# console applications.
4. Run the application, and you will be prompted to interact with the TODO list via the console.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
