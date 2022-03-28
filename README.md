# About
This application is designed to visualize various array sorting algorithms. The interface is made using WPF.
![NVIDIA_Share_J1Tna943CP](https://user-images.githubusercontent.com/91058562/160460431-58afb646-db1a-4527-83af-b5b18434705d.gif)
## How to use it
<b>That what you need to do to add your own array sorting algorithm:</b>
<ol>
  <li>Inherit from ISortAlgorithm</li>
  <li>Implement ISortAlgoritm like i did it in other algorithms</li>

a. Put this into start of your class

        WaitDelegate ISortAlgorithm.Wait { get => wait; set => wait += value; } 
        public string Title { get { return "{Algorithm name}"; } } 
        WaitDelegate wait;
        public event Action OnRedraw; 
        public event Action OnSortDone;

        
b. Insert OnReadraw() and wait() into the code snippet in which the array is changed
        
c. Insert OnSortDone() at the end of sort
  <li>Add your algorithm into collection of SortHost</li>
  
![image](https://user-images.githubusercontent.com/91058562/160454035-fff83190-7a33-4a79-b3c5-8ee6ab77e931.png)
  
  <li>Profit</li>
</ol>

