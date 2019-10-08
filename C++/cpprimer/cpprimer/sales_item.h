#pragma once

#include <iostream>
#include <string>

using namespace std;

class  Sales_item
{
	friend istream&operator>>(istream&, Sales_item&);
	friend ostream&operator<<(ostream&, const Sales_item&);
	friend bool operator<(const Sales_item&, const Sales_item&);
	friend bool operator==(const Sales_item&, const Sales_item&);

public:
#if defined(IN_CLASS_INITS)&&defined(DEFAULT_FCNS)
	Sales_item() = default;
#else
	Sales_item() :
		units_sold(0),
		revenue(0.0)
	{}
#endif
	Sales_item(const string &book) :
		bookNo(book),
		units_sold(0),
		revenue(0.0)
	{}
	Sales_item(istream &is)
	{
		is >> *this;
	}

public:
	Sales_item& operator+=(const Sales_item&);
	string isbn()const
	{
		return bookNo;
	}
	double avg_price()const;
private:
	string bookNo;
#ifdef IN_CLASS_INITS
	unsigned units_sold = 0;
	double revenue = 0.0;
#else
	unsigned units_sold;
	double revenue;
#endif
};

inline bool operator!=(const Sales_item&lhs, const Sales_item&rhs)
{
	return !(lhs == rhs);
}

Sales_item&Sales_item::operator+=(const Sales_item&rhs)
{
	units_sold += rhs.units_sold;
	revenue += rhs.revenue;
	return *this;
}

Sales_item operator+(const Sales_item lhs, const Sales_item&rhs)
{
	Sales_item ret(lhs);
	ret += rhs;
	return ret;
}

istream & operator >>(istream& in, Sales_item& s)
{
	double price;
	in >> s.bookNo >> s.units_sold >> price;
	if (in)
	{
		s.revenue = s.units_sold*price;
	}
	else
	{
		s = Sales_item();
	}
	return in;
}
ostream &operator <<(ostream&out, const Sales_item&s)
{
	out << s.isbn() << " " << s.units_sold << " " << s.revenue << " " << s.avg_price();
	return out;
}
double Sales_item::avg_price()const
{
	if (units_sold)
	{
		return revenue / units_sold;
	}
	else
	{
		return 0;
	}
}