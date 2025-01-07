
# Project Idea

1. Load the TODOs from the /api/v1/todos endpoint (GET request) into a List.
2. Display the available TODOs or show a message: "No TODOs found".
3. Prompt the user to choose an operation: (view / add / update / delete).

### 3.1. View:
- Calls the todo.DisplayDetails() method to display all details of the selected TODO.

### 3.2. Add:
- Prompts the user to enter the details of the TODO (name, importance, description, deadline).
- Converts the TODO into JSON format and sends a POST request to the server.
- Refreshes the TODO list (via a GET API call or using List.Add()).

### 3.3. Update:
- Displays the details of the selected TODO and asks the user which field to update.
- Prompts the user to provide a new value for the chosen field.
- Converts the updated TODO into JSON format and sends a PATCH request to the API.
- Refreshes the TODO list (via a GET API call or using List.Add()).

### 3.4. Delete:
- Displays the list of TODOs and asks the user which TODO should be deleted.
- Sends a DELETE request to /api/v1/todos/:id with the selected TODO's ID.
- Refreshes the TODO list (via a GET API call or using List.Remove()).
