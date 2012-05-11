#include <mega16.h>
#include "uart.h"
#include <delay.h>

#define ADSC ADCSRA.6
#define ADLAR ADMUX.5


void main()
{

    int adc_result;
    InitUART(9600, 8);      // UART'en initieres.
  
    ///// PWM ////////////////
    DDRD.5 = 1;             // Port D.5(OCR1A) initieres til en udgang.
    TCCR1A = 0b10000011;    // non-inverting
    TCCR1B = 0b00000010;    // Prescale s�ttes til 8. CS12, CS11 og CS10 s�ttes til hhv. 010
                              // Register TCCR1A og B initieres. "Phase Correct PWM mode", 10 bit
                              //
    OCR1A = 0;              // PWM signalet s�tter som udgangspunkt til nul. Dioden lyser ved maksimal styrke.
    //////////////////////////
  
    ////// ADC ///////////////
    ADMUX = 0b01000000;     // Multiplex'eren v�lger ADC0, ADLAR = 0, intern 5 volts referance.
    ADCSRA = 0b11000101;    // ADCSRA registeret t�ndes, div factor p� 32(frekvensen ligger mellem 50 og 200 Hz)
                            // Konverteringen s�ttes igang.
    //////////////////////////
  
    while(1)
    {
       
             //
        if(ReadChar() == 'R')
        {  
            ADCSRA.6 = 1;   // Konverteringen startes igen   
            ////// ADC /////////////////
            while (ADCSRA.6)          // Den springer f�rst over "while", n�r konverteringen er f�rdig.
            {}  
                     
            adc_result = ADCW;        // Resultatet fra ADC gemmes i en variabel.
            SendInteger(adc_result);  // Resultatet fra AD konverteringen sendes i terminalen.
            SendChar('\n');    
       
          
        }
    
    }
    
}
  