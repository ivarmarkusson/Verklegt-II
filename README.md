######################

# Verklegt-II
Project description	
Mooshak 2.0		
				
######################

### General ###

- It is important for students studying programming to get instant feedback when writing programs. Tools such as Mooshak are 
  extremely helpful, where a teacher can define assignments which state what program(s) students should write, as well as define   
  what the outcome should be for a given input, and where students can submit their solutions to said programs, and see clearly 
  if their solution is solving the problem correctly

- Such solutions must be modern and user-friendly. I.e. they must be usable on multiple devices, not just on laptop and 
  desktop machines, and it must be easy for users, both students and teachers, to use them. Students are usually already 
  occupied with learning the nuances of a new programming language, which means that it is essential that such a solution 
  will not get in their way. The same applies to teachers, which are already occupied with creating assignments and helping 
  material, and assisting students.

- The current Mooshak system is very good in many ways, but the user interface desperately needs an upgrade.

- It is therefore your task to create a simplified and user-friendly next version of Mooshak.


---------------------------------------------------------------------------------------------------------------------------------------------------


### Requirements ###

The system shall assume there are 3 types of users: Administrators, Teachers and Students.

- An Administrator runs the web application. (S)he should be able to create users and define their role, as well as create 
  courses and link users to those courses.

- Teachers must be able to create assignments within a course. Each assignment should optionally contain multiple parts, where 
  each part is X% of the grade for the assignment

- Each assignment part defines a program which should be written (example: a program which adds two numbers). For any program, 
  it should be possible to define input/output pairs, where a correct program implementation will give the specified output for 
  the given input. Example: a program which should add two numbers could have two such pairs defined, 
  the first has the input 5 and 4, output 9, and the second has the input 4 and -2, output 2. There should be no practical 
  limits to the number of input/output pairs associated with a given part.

- For each part, a student should be able to hand in his/her solution to that part. There should usually be no restrictions 
  to the number of submissions allowed from a given student for each part (however, see below), and the system should keep a 
  record of all the submissions, including if it was successful or not, such that students and/or teachers should be able to view this history.

- The teacher should be able to view the results, such that they can be moved someplace else (such as MySchool or to some other LMS system). 
  When listing the results for a given student, the best result for each assignment part should be listed. I.e. if the student handed in 3 solutions 
  to a given part, and the second solution turned out to be the one which gave the most correct answers.


----------------------------------------------------------------------------------------------------------------------------------------------------


The ability to accept a program text, execute the program (which could include compiling/linking it) with a given input, and compare the output 
to the expected output, is a quite complex operation.
It is possible to start with the given implementation, which encapsulates this functionality, but simply returns a random result:


/// <summary>
///
/// This method accepts two inputs:
///
/// 1. A program to execute. Example: 
///    "#include <iostream>\nint main() {\ncout << "Hello world"
///     << endl;\nreturn 0;\n}"
///
/// 2. A list of input/output pairs (the class InputOutputPair is
///    not defined here, but basically just contains two properties:
///    Input and Output.
///
/// It returns the number of input/output pairs which the program
/// processes correctly.
///
/// </summary>

public int GetNumberOfCorrectRuns(string programText, 
   List<InputOutputPair> pairs
   // Optional parameter: type of programming language)
{
   var random = new Random();

   // TODO: implementation left as an exercise for the reader,
   // this method returns a random value.

   return random.Next(0, pairs.Length);
}


The final implementation could simply be like this. However, this would effectively limit the maximum grade possible for your solution, 
i.e. it would be guaranteed to be no higher than 7 (no matter if there are other enhancements).

If you choose to try to execute the program, you are free to pick whatever programming language(s) you choose. Note that it may be easier 
to start with a scripting language (Python, JavaScript/Node.js, VBScript, etc.) instead of a language like C++ which requires compiling 
and linking, although you are of course free to experiment with it. The option to support multiple programming languages is also a bonus.

-----------------------------------------------------------------------------------------------------------------------------------------------

### Possible enhancements ###

- You are free to add functionality to your solution, possibly using some of the ideas below, but this is in no way an exhaustive list, i.e. 
  other enhancements may very well give bonus points.

- A teacher and/or students could define student groups, such that submissions from all group members are visible to other members.

- A teacher could define a limit to the number of submissions accepted for a given assignment part, or a penalty for a number of submissions 
  above a certain limit.

- The ability for students to edit their programs inside the application, optionally using libraries such as the Ace editor.

- The ability to debug programs inside the application (this is a big can of worms! however, it could be very helpful for students)

- The ability for students to edit the code online simultaneously (similar to Google Docs), such that group members don't have to be located 
  in the same building but can still cooperate when editing the code.

- The ability for students to invite their teacher to their code, such that the teacher can comment on the code (again: helpful if the students 
  and the teacher are not located in the same building).

- The ability to link this solution to the Centris LMS API, which defines users, courses and assignments. Note: this requires a special access, 
  which will be granted upon requests.

-----------------------------------------------------------------------------------------------------------------------------------------------

Note: it is important not to get too occupied with adding bonus features, while forgetting the main requirements. I.e. it is not guaranteed 
that bonus features will give any points if main requirements have not been met.

Published by Google Drive–Report Abuse–Updated automatically every 5 minutes