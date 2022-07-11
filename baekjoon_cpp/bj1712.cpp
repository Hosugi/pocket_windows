#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
int main() {
	int tax;                // 공장설립
	int materials, price;   //원가, 판매가격, 

	scanf("%d %d %d", &tax, &materials, &price);

	if (materials >= price) {       // 손익분기점을 넘지못하는 경우의 수
		printf("-1");
	}
	else {
	
		tax /= (price - materials);					//고정 지출 / 마진 = 손익분기점 넘는 순간	
		printf("%d", tax+1);						// 손익분기점과 같아지는 순간까지 더해주기
	}
}