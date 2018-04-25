# Position-Based-Dynamics

Position based dynamics is a method used to simulate physical phenomena like cloth, deformation, fluids, fractures, rigidness and much more. Most of the core math comes from [this](https://github.com/InteractiveComputerGraphics/PositionBasedDynamics) C++/OpenGL project over at GitHub.

 
The key process in PBD is to simulate the object as a set of points and constraints. Forces are applied to the points to move them and then the constraints make sure the points wont move in a way that violates the simulation.


I have included the code for cloth, deformable, fluid and rigid constraints but the GitHub project also contains some rope/chain constraints as well as a large variety of ball joint and hinges constraints.


All the code runs on the CPU so performance will be poor especially for the more demanding constraints like fluids. This project is more a example of how the math and code works rather than anything practical and the graphics are just line renderings or spheres.


PBD is also the method used for Nivida's [Flex](https://developer.nvidia.com/flex) but that runs on the GPU.

You can download a unity package [here](https://app.box.com/s/ek0000kfn1hzo2n3yp2mtkrfeujxj5fp).

Below is the PBD cloth scene. The points in the green bounds are static so the cloth will hang from those points.

![](https://static.wixstatic.com/media/1e04d5_4486dee7dc464bbb9dcee6edb4ec532c~mv2.jpg/v1/fill/w_550,h_550,al_c,q_80,usm_0.66_1.00_0.01/1e04d5_4486dee7dc464bbb9dcee6edb4ec532c~mv2.jpg)

Below is a PBD rigid body. Each point moves independently but the shape matching constraint will make sure the objects shape is still retained.

![](https://static.wixstatic.com/media/1e04d5_5094bf7934ef4a47bebdc2b8d3e9c07e~mv2.jpg/v1/fill/w_550,h_550,al_c,q_80,usm_0.66_1.00_0.01/1e04d5_5094bf7934ef4a47bebdc2b8d3e9c07e~mv2.jpg)

Below is a PBD deformable object. Its is made of tetrahedrons and bends. The points in the green bounds are static like in the cloth scene.

![](https://static.wixstatic.com/media/1e04d5_e4f76c0ddd164f879d65663cd9a298e7~mv2.jpg/v1/fill/w_550,h_550,al_c,q_80,usm_0.66_1.00_0.01/1e04d5_e4f76c0ddd164f879d65663cd9a298e7~mv2.jpg)

Below is PBD fluid. The spheres represent the fluids particles and they will flow together like in a fluid. You can see them forming a wave. Given enough particles it will look like water but its quite demanding to do on a single thread on the CPU so you will have to make do with a limited amount of particles.

![](https://static.wixstatic.com/media/1e04d5_7dea3eab707145c69d529e4ef9a7d3a6~mv2.jpg/v1/fill/w_550,h_550,al_c,q_80,usm_0.66_1.00_0.01/1e04d5_7dea3eab707145c69d529e4ef9a7d3a6~mv2.jpg)
