#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdint.h>
#include <errno.h>

#include <wiringPi.h>
//#include <wiringPiSPI.h>

#include <mysql/mysql.h>

#include <time.h>
#include <math.h>

#define MAXTIMINGS 85
#define DHTPIN 7

#define SPI_CHANNEL 0
#define SPI_SPEED 1000000

#define DBHOST "localhost"
#define DBUSER "root"
#define DBPASS "1234"
#define DBNAME "test"

MYSQL *connector;
MYSQL_RES *result;
MYSQL_ROW row;

int dht11_dat[5] = {0,0,0,0,0};


//온습도 읽기 dht11_dat[0]= 습도,dht11_dat[2]=온도 저장됨
void read_dh11_dat()
{
	uint8_t laststate	= HIGH;
	uint8_t counter		=0;
	uint8_t j		=0, i;
float	f; 
	dht11_dat[0] = dht11_dat[1] = dht11_dat[2] = dht11_dat[3] = dht11_dat[4] =0;
	pinMode( DHTPIN, OUTPUT );
	digitalWrite( DHTPIN, LOW );
	delay( 18 );
	digitalWrite( DHTPIN, HIGH );
	delayMicroseconds( 40 );
	pinMode( DHTPIN, INPUT );
for ( i =0; i < MAXTIMINGS; i++ )
	{
		counter =0;
while ( digitalRead( DHTPIN ) == laststate )
		{
			counter++;
			delayMicroseconds( 1 );
if ( counter ==255 )
			{
break;
			}
		}
		laststate = digitalRead( DHTPIN );
if ( counter ==255 )
break;
if ( (i >=4) && (i % 2==0) )
		{
			dht11_dat[j /8] <<=1;
if ( counter >50 )
				dht11_dat[j /8] |=1;
			j++;
		}
	}
if ( (j >=40) &&
	     (dht11_dat[4] == ( (dht11_dat[0] + dht11_dat[1] + dht11_dat[2] + dht11_dat[3]) &0xFF) ) )
	{
		f = dht11_dat[2] *9./5.+32;
printf( "Humidity = %d.%d %% Temperature = %d.%d C (%.1f F)\n",
			dht11_dat[0], dht11_dat[1], dht11_dat[2], dht11_dat[3], f );
	}else  {
printf( "Data not good, skip\n" );
	}
}

//Main
int main(void)
{	
	//wiringPiSetup
	if(wiringPiSetup()==-1)
	{
		fprintf(stdout,"Unable to start wiringPi:%s\n",strerror(errno));
		return 1;
	}
	/*
	if(wiringPiSPISetup(SPI_CHANNEL,SPI_SPEED)==-1)
	{
		fprintf(stdout,"wiringPiSPISetup Failed : %s\n",strerror(errno));
		return 1;
	}
*/
	//MySQL

	//MySQL 초기화
	connector = mysql_init(NULL);
	//MySQL 연결
	if(!mysql_real_connect(connector,DBHOST,DBUSER,DBPASS,DBNAME,3306,NULL,0))
	{
		fprintf(stderr,"%s\n",mysql_error(connector));
		return 0;
	}

	printf("MySQL(rpidb) opened.\n");

	int temp=0;
	while(1)
	{

		char query[1024];	//쿼리문을 담을 임시변수
		read_dh11_dat();	//온습도 센서값 읽음

		sprintf(query,"insert into line_stat values(now(),%d,%d)",dht11_dat[0], dht11_dat[2] );		//query에 쿼리문 넣기

		if(mysql_query(connector,query))//쿼리문을 mysql로 보내기
		{
			fprintf(stderr,"%s\n",mysql_error(connector));
			printf("Write DB error\n");
		}
		delay(1000);
		temp++;
		if(temp==100)
			break;
	}
	mysql_close(connector);	//MySQL 연결 끊기
	printf("종료합니다.");
	return 0;
}



