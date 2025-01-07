# Directory Display Application

This is a C# console application that recursively displays the contents (files and folders) of a specified directory. The program takes a directory path, checks if the directory exists, and lists all files and subfolders within it. The user can see the directory structure displayed hierarchically.

## Features
- **Recursive Directory Traversal**: The program goes through the specified directory and all of its subdirectories.
- **File and Folder Display**: It shows all files and folders within the given directory, with indentation indicating their hierarchical structure.
- **Customizable Path**: The user can modify the directory path to display the contents of any accessible directory.

## How It Works
1. The program takes a path as a string (e.g., `"C:\Windows\Globalization"`) and displays its contents.
2. It checks whether the directory exists.
3. The program will recursively list:
    - Files: Displayed with one level of indentation.
    - Subdirectories: Displayed with deeper indentation, and the program will recurse into each subdirectory.
4. If the specified directory does not exist, an error message will be shown.

## Key Changes
- The path variable (which points to a directory) can be changed to any desired directory.
- The program will recursively display all files and subdirectories in the provided directory path.

## Requirements
- A C# compatible development environment (e.g., Visual Studio, Visual Studio Code, or Rider).
- Access to the specified directory on your machine.

## Installation
1. Clone the repository to your local machine.
2. Open the project in your preferred C# IDE.
3. Update the Directory Path: string path = @"C:\Windows\Globalization"
4. Run the application in the terminal/console.
5. The program will display the directory structure for the specified path, including all files and subdirectories.