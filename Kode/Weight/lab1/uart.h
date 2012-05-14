/*----------------------------------------------------------------------
  File name : "uart.h"
  
  Header file for Extended, polled UART-driver.
  Target = Mega16 USART.
  
  Author : Henning Hargaard
  Date : 24.10.2008
-----------------------------------------------------------------------*/
void InitUART( unsigned long BaudRate, unsigned char DataBit );
unsigned char CharReady();
char ReadChar();
void SendChar(char Tegn);
void SendStringFlash(flash char* Streng);
void SendStringSRAM(char* Streng);
void SendInteger(int Tal);
//----------------------------------------------------------------------