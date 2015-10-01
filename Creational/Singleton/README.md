# Prototype

**Singleton** is a creational design pattern that lets you ensure that a class has only one instance, while providing a global access point to this instance.

Frequency of use ![medium-high](./img/use_medium_high.gif)

## Intent
* Ensure a class has only one instance, and provide a global point of access to it.
* Encapsulated "just-in-time initialization" or "initialization on first use".

## Problem
Application needs one, and only one, instance of an object. Additionally, lazy initialization and global access are necessary.

## Structure
![structure](./img/structure.png)

## Participants
The classes and objects participating in this pattern include:

* **Singleton** (*LoadBalancer*)
  * defines an Instance operation that lets clients access its unique instance. Instance is a class operation.
responsible for creating and maintaining its own unique instance.
