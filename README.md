# The Command Pattern

Command is a behavioral design pattern that turns a request into a stand-alone object that contains all information about the request.

In this pattern, an abstract Command class is declared as an interface for executing operations. This Command class defines a method, named execute, which must be implemented in each concrete command. 

**Note:**
* An adaptation of the pattern is to implement a generic execute method in the abstract command class and delegate to the concrete classes to implement other specific methods, like in our implementation: Delete, Inset and Update.

This execute method is a bridge between a Receiver object and an action. The Receiver knows how to perform the operations associated with a request (any class may be a Receiver). Another relevant component in this pattern is the Invoker class, which asks for the command that must be executed.

 **Pros**
* Single Responsibility Principle. You can decouple classes that invoke operations from classes that perform these operations.
* Open/Closed Principle. You can introduce new commands into the app without breaking existing client code.
* You can implement undo/redo.
* You can implement deferred execution of operations.
* You can assemble a set of simple commands into a complex one.
 
 **Cons**
* The code may become more complicated since you’re introducing a whole new layer between senders and receivers.