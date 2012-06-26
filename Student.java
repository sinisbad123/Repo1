package domain;

public class Student
{
	private String name;
	private String course;
	private int age;
	
	public void setName(String n)
	{
		name = n;
	}
	
	public void setCourse(String c)
	{
		course = c;
	}
	
	public void setAge(int a)
	{
		if (a >= 0)
			age = a;
		else
			age = 0;
	}
	
	public String getName()
	{
		return name;
	}
	
	public String getCourse()
	{
		return course;
	}
	
	public int getAge()
	{
		return age;
	}
}