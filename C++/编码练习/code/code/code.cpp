// code.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <string>

using namespace std;

//思路一，暴力移位
void leftshiftone(char*s, int n) //以前n位左旋一位
{
	char t = s[0];
	for (int i = 1; i < n; i++)
	{
		s[i - 1] = s[i];
	}
	s[n - 1] = t;
}

void leftshiftone(char*s)//自动计算长度，左旋一位
{
	int n = 0;
	char t = s[0];

	while (*(s + n) != '\0')
	{
		n++;
	}
	for (int i = 1; i < n; i++)
	{
		s[i - 1] = s[i];
	}
	s[n - 1] = t;
}
void leftshift(char*s, int n, int m) //左旋m位数字
{
	while (m--)
	{
		leftshiftone(s, n);
	}
}

//思路二，指针翻转

void rotate1(string &str, int m)
{
	if (str.length() == 0 || m <= 0)
		return;
	
	int n = str.length();

	if (m > n)
		return;

	int p1 = 0, p2 = m;
	int k = (n - m) - n % m; 
	// abc def ghi j
	// def ghi abc j
	// def ghi jbc a  //按照while逻辑，尾部 jbc a，所以k减去不能用while逻辑处理的部分
	while (k--)
	{
		swap(str[p1], str[p2]);
		p1++;
		p2++;
	}

	//尾部特殊处理，依次向前交换
	int r = n - p2;  //尾部剩余元素数
	while (r--)
	{
		int i = p2;
		while (i>p1)
		{
			swap(str[i], str[i - 1]);
			i--;
		}
		p1++;
		p2++;
	}
}
void rotate2(string &str, int m)
{
	if (str.length() == 0 || m <= 0)
		return;

	int n = str.length();

	if (m > n)
		return;

	int p1 = 0, p2 = m;
	int k = (n - m);
	// abc def ghi j
	// def ghi abc j
	// def ghi jbc a  //按照while逻辑，尾部 jbc a
	while (k--)
	{
		swap(str[p1], str[p2]);
		p1++;
		p2++;
	}

	//尾部特殊处理，依次向前交换
	int r = m - n % m;

	while (r--)
	{
		int index = p1;
		while (index < p2 - 1)
		{
			swap(str[index], str[index + 1]);
			index++;
		}
	}
}

//思路三，左右递归旋转，待旋转的字符串长度不断缩减，问题的规模不断减小
void rotate(string &str, int n,int m,int head,int tail,bool flag)
{
	if (head == tail || m <= 0)
		return;

	if (flag == true)
	{
		int p1 = head;
		int p2 = head + m;
		int k = (n - m) - n % m;

		for (int i = 0; i < k; i++, p1++, p2++)
			swap(str[p1], str[p2]);

		rotate(str, n - k, n%m, p1, tail, false);
	}
	else
	{
		int p1 = tail;
		int p2 = tail - m;
		int k = (n - m) - n % m;

		for (int i = 0; i < k; i++, p1--, p2--)
			swap(str[p1], str[p2]);
		rotate(str, n - k, n%m, head, p1, true);
	}
}



int main()
{

	//第一种，暴力旋转
	char arr[] = "abcdefg";

	leftshiftone(arr, 1);
	cout << arr << endl;

	leftshiftone(arr);
	cout << arr << endl;
	
	leftshift(arr, sizeof(arr) - 1, 3);
	cout << arr << endl;

	//第二种，指针交换

	string st1 = "abcdefghij";
	string st2 = "abcdefghij";

	rotate1(st1, 3);
	rotate2(st2, 4);
	cout << st1 << endl;
	cout << st2 << endl;

}