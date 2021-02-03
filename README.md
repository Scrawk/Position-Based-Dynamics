# Position-Based-Dynamics

Position based dynamics is a method used to simulate physical phenomena like cloth, deformation, fluids, fractures, rigidness and much more. Most of the core math comes from [this](https://github.com/InteractiveComputerGraphics/PositionBasedDynamics) C++/OpenGL project over at GitHub.

 
The key process in PBD is to simulate the object as a set of points and constraints. Forces are applied to the points to move them and then the constraints make sure the points wont move in a way that violates the simulation.


I have included the code for cloth, deformable, fluid and rigid constraints but the GitHub project also contains some rope/chain constraints as well as a large variety of ball joint and hinges constraints.


All the code runs on the CPU so performance will be poor especially for the more demanding constraints like fluids. This project is more a example of how the math and code works rather than anything practical and the graphics are just line renderings or spheres.


PBD is also the method used for Nivida's [Flex](https://developer.nvidia.com/flex) but that runs on the GPU.

Below is the PBD cloth scene. The points in the green bounds are static so the cloth will hang from those points.

![PBDCloth](./Media/PBDCloth.jfif)

Below is a PBD rigid body. Each point moves independently but the shape matching constraint will make sure the objects shape is still retained.

![PBDRigid](./Media/PBDRigid.jfif)

Below is a PBD deformable object. Its is made of tetrahedrons and bends. The points in the green bounds are static like in the cloth scene.

![PBDDeformable](./Media/PBDDeformable.jfif)

Below is PBD fluid. The spheres represent the fluids particles and they will flow together like in a fluid. You can see them forming a wave. Given enough particles it will look like water but its quite demanding to do on a single thread on the CPU so you will have to make do with a limited amount of particles.

![PBDFluid](./Media/PBDFluid.jfif)
