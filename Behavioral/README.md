# Behavioral Design Patterns

Behavioral design patterns are concerned with algorithms and the assignment of responsibilities between objects.

* [**Chain of Responsibility**](./ChainOfResponsibility/)

  ![chain-of-responsibility.png](./img/chain-of-responsibility.png)

  Lets you pass requests along a chain of handlers. Upon receiving a request, each handler decides either to process the request or to pass it to the next handler in the chain.

* [**Command**](./Command/)

  ![command.png](./img/command.png)

  Turns a request into a stand-alone object that contains all information about the request. This transformation lets you pass requests as a method arguments, delay or queue a request's execution, and support undoable operations.

* [**Interpreter**](./Interpreter/)

  ![interpreter.png](./img/interpreter.png)

  Given a language, defines a representation for its grammar along with an interpreter that uses the representation to interpret sentences in the language. 

* [**Iterator**](./Iterator/)

  ![iterator.png](./img/iterator.png)

  Lets you traverse elements of a collection without exposing its underlying representation (list, stack, tree, etc.).

* [**Mediator**](./Mediator/)

  ![mediator.png](./img/mediator.png)

  Lets you reduce chaotic dependencies between objects. The pattern restricts direct communications between the objects and forces them to collaborate only via a mediator object.

* [**Memento**](./Memento/)

  ![memento.png](./img/memento.png)

  Lets you save and restore the previous state of an object without revealing the details of its implementation.

* [**Observer**](./Observer/)

  ![observer.png](./img/observer.png)

  Lets you define a subscription mechanism to notify multiple objects about any events that happen to the object they're observing.

* [**State**](./State/)

  ![state.png](./img/state.png)

  Lets an object alter its behavior when its internal state changes. It appears as if the object changed its class.

* [**Strategy**](./Strategy/)

  ![strategy.png](./img/strategy.png)

  Lets you define a family of algorithms, put each of them into a separate class, and make their objects interchangeable.

* [**Template Method**](./TemplateMethod/)

  ![template-method.png](./img/template-method.png)

  Defines the skeleton of an algorithm in the superclass but lets subclasses override specific steps of the algorithm without changing its structure.

* [**Visitor**](./Visitor/)

  ![visitor.png](./img/visitor.png)

  Lets you separate algorithms from the objects on which they operate.