# Program

The graduation research by the speciality System Analysis, Applied Mathematics Department, Master’s degree.

The work is interesting for study of a well-known class of optimization applied problems of distribution of partially ordered works, the execution time of which is a non-constant value, reduced to the generalized problem of constructing parallel orders, analysis of anomaly examples, study of the conditions for the absence of anomalies and their appearance, development of a method according to which for individual cases of graphs, it is possible to change the initial parameters of the problem so that anomalies do not occur, detection of how changing other initial data affects the appearance of anomalies, software implementation of algorithms that illustrate the occurrence of anomalies for user-specified input data, analysis of program results.

The obtained results can be used in many different areas related to step-by-step processing of information, parallelization of calculations or manufacturing of consumer products of various kinds, where it is necessary to adjust the organization of the functioning of complex systems.

More details can be found at the following link: https://github.com/PollyVas/Graph-anomalies/blob/master/About%20Research

Further, you can explore examples of different types of anomalies investigated in this work.

Type 1 anomaly occurs when a vertex with a higher weight compared to others is given higher priority and moved to the left in the list L. At first glance, this should lead to a reduction in execution time or optimization of resource allocation. However, as a result of this movement, the opposite effect may occur—the overall execution time or resource usage increases.

This happens because shifting a vertex with a higher priority to the left can change the execution order of dependent tasks or cause other important tasks to be executed later than necessary for optimizing the process. As a result, instead of improving distribution, this can lead to delays or system overload.

![Аномалія 1  Умови ланцюжок](https://github.com/user-attachments/assets/da6f0586-51f6-4593-a6ed-da66543b2b86)

Type 2 anomaly occurs when edges are removed from the graph.

![3 3 Аномалія 2](https://github.com/user-attachments/assets/4ebdb6ae-33e1-46e6-9eed-a971a553d157)

Type 3 anomaly occurs when a constant value is subtracted from the weight of each vertex in the graph.

![Аномалія 3](https://github.com/user-attachments/assets/abb0ee02-0a97-4eae-9f91-82242a62c644)

Type 4 anomaly occurs when the width of the ordering is increased.

![Аномалія 4](https://github.com/user-attachments/assets/c0d7f25e-a7b8-41e3-9b50-ab544e21e75d)

The new anomaly proposed in the work occurs when vertices are removed from the graph along with their incoming and outgoing edges. From a practical standpoint, removing a vertex reduces the total amount of work, and the removal of edges weakens the technological constraints, which should theoretically reduce the overall execution time. However, as shown in Example 15, in some cases, an anomaly arises, and the length of the ordering 𝑙(𝑆) increases. This anomaly can be seen as a generalization of Graham's anomaly, where the weakening of the partial order in the graph leads to unexpected results (Type 2).

![Аномалія 5](https://github.com/user-attachments/assets/28186c66-b583-46f7-9f97-35d4aa1ef543)


