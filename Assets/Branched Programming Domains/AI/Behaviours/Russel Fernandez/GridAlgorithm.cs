/*
Creating a Grid with dijkstra algorithm
List<int> Neighbors = new List<int> Neighbors();
Dictionary<int,List<int> Neighbors> Grid = new Dictionary<int, List<int> Neighbors>();//initialize 2d Grid

Distance = Destination - CurrentPosition;

int BiggestValue = 0;
int CurrentGridNum = 0;
int GridPos = 1;
int CurrentDistance = 1;

Grid.Add(0,new List<int> Neighbors());//Create first key '0'
Grid[0].Add(-1,-1,1,1)// add values to list, each index in list corresponds to which neighbor. -1 means empty
//0 = North, 1 = East, 2 = South, 3 = West
// Now grid is initialized with a start position(first key) of 0 at the top right corner of the grid.


for(int i = 0; i < distance; i++) // Fill Key values with Grid numbers, each key is a 1x1 square on the grid
{
Grid.Add(GridPos, List<int> Neighbors());
GridPos++;
}

for (int i = 0; i < Grid.Count; i++) //add distance to each neighbor
{
    Grid.Keys.ElementAt(i),
    if(Grid[Grid.Keys.ElementAt(i)] isEmpty && Grid[Grid.Keys.ElementAt(i)] > Grid[Grid.Keys.ElementAt(i-1)])
        {
        Grid[i].Add(CurrentDistance)
        CurrentDistance++;       
        }
}

for (int i = 0; i < Grid.Count; i++) //iterates through dictionary
{
    Grid.Keys.ElementAt(i),
    if( Grid[Grid.Keys.ElementAt(i)] > BiggestValue) //if neighbors value(distance) is greater than the current biggestvalue
    //, set biggestvalue to that number
        {
        BiggestValue = Grid[Grid.Keys.ElementAt(i)];
               
        }




    if(BiggestValue == Distance && Grid.Keys.ElementAt(i) == Distance)   
    {
    return 0; //stops adding to grid if Distance = biggest value on a grid square
    }
}











*/