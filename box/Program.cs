public class Box
{
    private double width, height, length;

    public Box(double width, double height, double length)
    {
        this.width = width;
        this.height = height;
        this.length = length;
    }

    public Box(double side)
    {
        this.width = side;
        this.height = side;
        this.length = side;
    }

    public Box(Box oldBox)
    {
        this.width = oldBox.width;
        this.height = oldBox.height;
        this.length = oldBox.length;
    }

    private double FaceArea()
    {
        return width * height;
    }

    private double TopArea()
    {
        return width * length;
    }

    private double SideArea()
    {
        return height * length;
    }

    public double Area()
    {
        return 2 * FaceArea() + 2 * TopArea() + 2 * SideArea();
    }
}

class Program
{
    static void Main()
    {
        Box box1 = new Box(2.0, 3.0, 4.0); 
        Box box2 = new Box(5.0);        
        Box box3 = new Box(box1);       

        Console.WriteLine("Area of box1: " + box1.Area());
        Console.WriteLine("Area of box2 (cube): " + box2.Area());
        Console.WriteLine("Area of box3 (copy of box1): " + box3.Area());
    }
}
