# Visitor

**Visitor** is a behavioral design pattern that lets you separate algorithms from the objects on which they operate.

Frequency of use ![low](./img/use_low.gif)

## Intent
* Represent an operation to be performed on the elements of an object structure. Visitor lets you define a new operation without changing the classes of the elements on which it operates.
* The classic technique for recovering lost type information.
* Do the right thing based on the type of two objects.
* Double dispatch

## Problem
Many distinct and unrelated operations need to be performed on node objects in a heterogeneous aggregate structure. You want to avoid "polluting" the node classes with these operations. And, you don't want to have to query the type of each node and cast the pointer to the correct type before performing the desired operation.

## Structure
![structure](./img/structure.png)

## Participants
The classes and objects participating in this pattern are:

* **Visitor** (*Visitor*)
  * declares a Visit operation for each class of ConcreteElement in the object structure. The operation's name and signature identifies the class that sends the Visit request to the visitor. That lets the visitor determine the concrete class of the element being visited. Then the visitor can access the elements directly through its particular interface
* **ConcreteVisitor** (*IncomeVisitor*, *VacationVisitor*)
  * implements each operation declared by Visitor. Each operation implements a fragment of the algorithm defined for the corresponding class or object in the structure. ConcreteVisitor provides the context for the algorithm and stores its local state. This state often accumulates results during the traversal of the structure.
* **Element** (*Element*)
  * defines an Accept operation that takes a visitor as an argument.
* **ConcreteElement** (*Employee*)
  * implements an Accept operation that takes a visitor as an argument
* **ObjectStructure** (*Employees*)
  * can enumerate its elements
  * may provide a high-level interface to allow the visitor to visit its elements
  * may either be a Composite (pattern) or a collection such as a list or a set