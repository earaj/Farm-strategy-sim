# Mathurin's Farm - Procedural Generation & AI Simulation

A Unity-based farming simulation demonstrating procedural content generation algorithms, AI state machines, and software architecture patterns.

<p align="center">
  <img width="48%" src="https://github.com/user-attachments/assets/ae124053-64aa-478c-a02f-f3df86e8237e" />
  &nbsp;
  <img width="48%" src="https://github.com/user-attachments/assets/6abb475f-72b6-46d6-8819-4564b196848f" />
</p>

<p align="center">
  <img width="80%" src="https://github.com/user-attachments/assets/3f5e508a-c8c9-4c22-80f3-1064f5828e1e" />
</p>

<p align="center">
  <img width="30%" src="https://github.com/user-attachments/assets/5d1eaed1-0209-4c82-84d9-c01858242493" />
  &nbsp;
  <img width="30%" src="https://github.com/user-attachments/assets/9410d1bd-ce40-4425-8888-323d8e64ff2b" />
  &nbsp;
  <img width="30%" src="https://github.com/user-attachments/assets/d2168e82-9fd1-40eb-80b9-2f90afb9c2ca" />
</p>

## 📖 About
This project was developed to explore advanced Unity mechanics and software engineering concepts within a game context. Unlike a standard farming simulator, the core focus of this project is on runtime algorithm switching and autonomous agent behaviors.

The player controls a farmer (Mathurin) who must manage resources, harvest timber using physics-based interactions, and protect livestock from time-dependent predators.

## ⚙️ Technical Implementation

### 1. Procedural Forest Generation (Strategy Pattern)
The project utilizes the Strategy Design Pattern to allow the forest generation algorithm to be swapped at runtime via the UI. This demonstrates decoupling the generation logic from the game manager.

The three implemented algorithms are:
* **Linear Generation:** Instantiates trees in fixed rows (O(n) complexity).
* **Randomized Distribution:** Uses spatial verification (Circle/Rect overlap checks) to ensure trees are placed randomly without mesh clipping.
* **Cellular Automata (Forest Simulation):** Implements a "Game of Life" style algorithm. The simulation runs for 10 generations to create organic, clustered forest growth that mimics natural vegetation patterns.

| Linear (Grid) | Cellular Automata (Simulated) |
| :---: | :---: |
| <img width="100%" src="https://github.com/user-attachments/assets/383aa38e-7f46-42d6-84b2-7536e134823d" /> | <img width="100%" src="https://github.com/user-attachments/assets/7a0906b4-6b91-4424-821b-ccb78d9815d3" /> |

### 2. Artificial Intelligence (FSM & NavMesh)
* **Fox AI (Finite State Machine):** A predator appears strictly between 21:00 and 08:00. It utilizes a state machine to switch between:
    * **Patrol State:** Moving between randomized waypoints on the map.
    * **Chase State:** Triggered when within 5 units of a target (Chicken). Overrides patrol logic to pursue the target dynamically.
* **Companion AI:** Chickens utilize Unity's NavMesh Agent to follow the player from the store to the farm enclosure before reverting to their wandering behavior.

### 3. Physics & Math
* **Tree Felling:** Instead of simple object destruction, trees utilize quaternion math (`Quaternion.Slerp`) to simulate a falling animation over a set duration, triggered by player interaction.
* **Trajectory Calculation:** Resource drops (apples/burgers) calculate gravity-based trajectories to land naturally on the terrain.

### 4. Quality Assurance (Unit Testing)
The project includes Unity Play Mode Tests (NUnit) to validate the integrity of core game mechanics, including:
* Inventory management logic.
* Growth cycle timers.
* Replanting validation.

## 🎮 Gameplay Features
* **Day/Night Cycle:** Dynamic environment changes that trigger specific enemy events.
* **Character Selection:** Modular character loading system.
* **Resource Management:** Economic system involving egg harvesting, wood selling, and energy management.

## 🛠 Tech Stack
* **Engine:** Unity 2022.3 LTS (C#)
* **Patterns:** Strategy, Singleton, Finite State Machine (FSM)
* **Tools:** Unity NavMesh, NUnit Framework
