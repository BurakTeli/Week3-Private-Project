using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Multi-Functional Program!");

        try
        {
            // Display menu options to the user
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1 - Calculator");
            Console.WriteLine("2 - Random Number Guessing Game");
            Console.WriteLine("3 - Average Grade Calculation");

            // Get user's choice
            Console.Write("Your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            // Redirect to the corresponding functionality
            switch (choice)
            {
                case 1:
                    RunCalculator();
                    break;
                case 2:
                    RunRandomNumberGame();
                    break;
                case 3:
                    RunAverageCalculation();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select between 1 and 3.");
                    break;
            }
        }
        catch (FormatException)
        {
            // Handle invalid input format
            Console.WriteLine("Error: Please enter a valid number.");
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        Console.WriteLine("Program has ended. Press any key to exit.");
        Console.ReadKey();
    }

    static void RunCalculator()
    {
        Console.WriteLine("Welcome to the Calculator Application!");

        try
        {
            // Get the first number from the user
            Console.Write("Enter the first number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());

            // Get the second number from the user
            Console.Write("Enter the second number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            // Display operation options
            Console.WriteLine("\nSelect the operation you want to perform:");
            Console.WriteLine("1 - Addition");
            Console.WriteLine("2 - Subtraction");
            Console.WriteLine("3 - Multiplication");
            Console.WriteLine("4 - Division");
            Console.WriteLine("5 - Square of the first number");

            // Get user's operation choice
            Console.Write("Your choice (1-5): ");
            int operation = Convert.ToInt32(Console.ReadLine());

            // Perform the selected operation
            switch (operation)
            {
                case 1:
                    Console.WriteLine($"Result: {num1} + {num2} = {num1 + num2}");
                    break;
                case 2:
                    Console.WriteLine($"Result: {num1} - {num2} = {num1 - num2}");
                    break;
                case 3:
                    Console.WriteLine($"Result: {num1} * {num2} = {num1 * num2}");
                    break;
                case 4:
                    if (num2 != 0)
                    {
                        Console.WriteLine($"Result: {num1} / {num2} = {(double)num1 / num2}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Division by zero is not allowed.");
                    }
                    break;
                case 5:
                    Console.WriteLine($"Result: {num1}^2 = {num1 * num1}");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select between 1 and 5.");
                    break;
            }
        }
        catch (FormatException)
        {
            // Handle invalid input format
            Console.WriteLine("Error: Please enter a valid number.");
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    static void RunRandomNumberGame()
    {
        Random random = new Random();
        int targetNumber = random.Next(1, 101); // Generate a random number between 1 and 100
        int guessLimit = 5; // Number of attempts allowed
        int guess;

        Console.WriteLine("Welcome to the Random Number Guessing Game!");
        Console.WriteLine("The computer has chosen a number between 1 and 100. You have 5 attempts to guess it!");

        for (int attemptsLeft = guessLimit; attemptsLeft > 0; attemptsLeft--)
        {
            Console.Write($"\nYou have {attemptsLeft} attempts left. Enter your guess: ");

            // Get the user's guess
            if (int.TryParse(Console.ReadLine(), out guess))
            {
                if (guess == targetNumber)
                {
                    Console.WriteLine("Congratulations! You guessed the correct number.");
                    return; // End the game
                }
                else if (guess < targetNumber)
                {
                    Console.WriteLine("Go higher!");
                }
                else
                {
                    Console.WriteLine("Go lower!");
                }
            }
            else
            {
                // Handle invalid input
                Console.WriteLine("Please enter a valid number!");
                attemptsLeft++; // Do not count invalid input as an attempt
            }
        }

        // Inform the user of the correct number after all attempts are used
        Console.WriteLine($"\nSorry, you ran out of attempts. The correct number was: {targetNumber}");
    }

    static void RunAverageCalculation()
    {
        Console.WriteLine("Welcome to the Average Calculation Program!");

        try
        {
            // Get the grades from the user
            Console.Write("Enter your first grade (0-100): ");
            double grade1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter your second grade (0-100): ");
            double grade2 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter your third grade (0-100): ");
            double grade3 = Convert.ToDouble(Console.ReadLine());

            // Validate the grades
            if (grade1 < 0 || grade1 > 100 || grade2 < 0 || grade2 > 100 || grade3 < 0 || grade3 > 100)
            {
                Console.WriteLine("Error: Grades must be between 0 and 100.");
                return;
            }

            // Calculate the average using double type
            double average = (grade1 + grade2 + grade3) / 3;
            Console.WriteLine($"\nYour average: {average:F2}");

            // Determine the letter grade and message
            string letterGrade = GetLetterGrade(average);

            // Get the additional message based on the letter grade
            string additionalMessage = GetAdditionalMessage(letterGrade);

            // Print the results
            Console.WriteLine($"Your Letter Grade: {letterGrade}");
            Console.WriteLine(additionalMessage);
        }
        catch (FormatException)
        {
            // Handle invalid input format
            Console.WriteLine("Error: Please enter a valid number.");
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    static string GetLetterGrade(double average)
    {
        // Define grade ranges and corresponding letter grades
        var grades = new (double min, double max, string letterGrade)[]
        {
            (90, 100, "AA"),
            (85, 89, "BA"),
            (80, 84, "BB"),
            (75, 79, "CB"),
            (70, 74, "CC"),
            (65, 69, "DC"),
            (60, 64, "DD"),
            (55, 59, "FD"),
            (0, 54, "FF")
        };

        // Return the appropriate letter grade based on the average
        foreach (var (min, max, letterGrade) in grades)
        {
            if (average >= min && average <= max)
            {
                return letterGrade;
            }
        }

        return "FF"; // Fallback (this case should never occur, added for safety)
    }

    static string GetAdditionalMessage(string letterGrade)
    {
        // Return an appropriate message based on the letter grade
        if (letterGrade == "AA")
        {
            return "Ajda Pekkan Gibisin";
        }
        else if (letterGrade == "BB" || letterGrade == "BA")
        {
            return "Ajda Pekkan Seninle Gurur Duyuyor ";
        }
        else if (letterGrade == "CB" || letterGrade == "CC" || letterGrade == "DC" || letterGrade == "DD")
        {
            return "Ajda Pekkan Sana Ksmüş Haberin Olsun";
        }
        else
        {
            return "Ajda Pekkan Sana Küfür Edecek Haberin Olsun Kendini Hazırla";
        }
    }
}
