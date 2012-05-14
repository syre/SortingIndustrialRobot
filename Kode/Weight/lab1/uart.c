/*********************************************
   CPA-A, LAB7
   
   Filnavn : "uart.c"

   EXTENDED, polled driver for Mega16's USART
   The prototypes are defined in the header "uart.h".
      
   STK500 setup :
   
   Header "RS232 spare" connected to RXD/TXD :
   RXD = PORTD, bit0
   TXD = PORTD, bit1

   CodeVisionAVR C Compiler
   
   Henning Hargaard 26/10 2008
**********************************************/

#include <mega16.h>
#include <stdlib.h>

// Constants
#define XTAL 3686400  

/*************************************************************************
USART initilization.
         Asyncronous mode.
	RX and TX enabled.
	No interrupts enabled.
	Baud rate = Parameter.
	Data bits = Parameter.
	Number of Stop Bits = 1.
	No Parity.
Parameters:
	BaudRate: Wanted Baud Rate.
	Databits: Wanted number of Data Bits.
*************************************************************************/
void InitUART( unsigned long BaudRate, unsigned char DataBit )
{
unsigned int TempUBRR;

  if ((BaudRate >= 110) && (BaudRate <= 115200) && (DataBit >=5) && (DataBit <= 8))
  { 
    // "Normal" clock, no multiprocesser mode (= default)
    UCSRA = 0b00100000;
    // No interrupts enabled
    // Receiver enabled
    // Transmitter enabled
    // No 9 bit operation
    UCSRB = 0b00011000;	
    // Asynchronous operation, 1 stop bit, no parity
    // Bit7 always has to be 1
    // Bit 2 and bit 1 controlles the number of databits
    UCSRC = 0b10000000 | (DataBit-5)<<1;
    // Set Baud Rate according to the parameter BaudRate:
    // Select Baud Rate (first store "UBRRH--UBRRL" in local 16-bit variable,
    //                   then write the two 8-bit registers seperately):
    TempUBRR = XTAL/(16*BaudRate) - 1;
    // Write upper part of UBRR
    UBRRH = TempUBRR >> 8;
    // Write lower part of UBRR
    UBRRL = TempUBRR;
  }  
}

/*************************************************************************
  Returns 0, if the UART has NOT received a new character.
  Returns value <> 0, if the UART HAS received a new character.
*************************************************************************/
unsigned char CharReady()
{
   return UCSRA.7;
}

/*************************************************************************
Awaits new character received.
Then this character is returned.
*************************************************************************/
char ReadChar()
{
  // Wait for new character received
  while ( UCSRA.7 == 0 )
  {}                        
  // Then return it
  return UDR;
}

/*************************************************************************
Awaits transmitter-register ready.
Then it send the character.
Parameter:
     Tegn: Character for sending. 
*************************************************************************/
void SendChar(char Tegn)
{
  // Wait for transmitter register empty (ready for new character)
  while ( UCSRA.5 == 0 )
  {}
  // Then send the character
  UDR = Tegn;
}

/*************************************************************************
Sends 0-terminated string (stored in FLASH memory).
Parameter:
   Streng: Pointer to the string. 
*************************************************************************/
void SendStringFlash(flash char* Streng)
{
  // Repeat untill zero-termination
  while (*Streng != 0)
  {
    // Send the character pointed to by "Streng"
    SendChar(*Streng);
    // Advance the pointer one step
    Streng++;
  }
}

/*************************************************************************
Sends 0-terminated string (stored in SRAM memory).
Parameter:
   Streng: Pointer to the string. 
*************************************************************************/
void SendStringSRAM(char* Streng)
{
  // Repeat untill zero-termination
  while (*Streng != 0)
  {
    // Send the character pointed to by "Streng"
    SendChar(*Streng);
    // Advance the pointer one step
    Streng++;
  }
}

/*************************************************************************
Converts the integer "Tal" to an ASCII string - and then sends this string
using the USART.
Makes use of the C standard library <stdlib.h>.
Parameter:
      Tal: The integer to be converted and send. 
*************************************************************************/
void SendInteger(int Tal)
{
char array[7];
  // Convert the integer til an ASCII string (array) 
  itoa(Tal, array);
  // - then send the string
  SendStringSRAM(array);
}
