
#include <iostream>
#include <string>

int main()
{
	std::string input;
	std::cin >> input;
	int stringLen = input.length();
	std::string encoded;
	int charCounter = 0;

	for (int i = 0; i < stringLen; ++i)
	{
		std::string current = input.substr(i, 1);
		if (std::islower(input[i]))
		{

			if (input[i] == input[i + 1])
			{
				charCounter++;
			}
			else
			{
				charCounter++;
				encoded.append(std::to_string(charCounter));
				encoded.append(current);
				charCounter = 0;
			}
		}
	}
	std::cout << encoded;


}



