#define SENSOR_PIN A0
#define PUMP_PIN 12
#define GREEN_LED_PIN 7
#define RED_LED_PIN 5
#define SENSOR_TRIGGER 25

int sensorValue = 0;
int debouncedValue = 0;
int prevValue = 0;
unsigned long prevTime = 0;
const unsigned long debounceDelay = 50;

unsigned long blinkInterval = 500;
unsigned long blinkTime = 0;
bool blinkState = false;

void setup() {
  // Inicializando sensor
  pinMode(SENSOR_PIN, INPUT);

  // Inicializando bomba d'agua
  pinMode(PUMP_PIN, OUTPUT);

  // Inicia bomba desligada
  digitalWrite(PUMP_PIN, HIGH);

  // Inicializando leds
  pinMode(GREEN_LED_PIN, OUTPUT);
  pinMode(RED_LED_PIN, OUTPUT);

  Serial.begin(9600);
}

void loop() {
  // Lê valor do sensor e converte para porcentagem
  sensorValue = map(analogRead(SENSOR_PIN), 0, 1023, 100, 0);

  if (Serial.available() > 0) {
    String command = Serial.readStringUntil('\n');
    
    if (command == "read_humidity") {
      Serial.print(sensorValue);
      Serial.println();
    }
    
    if (command == "trigger_pump") {
      digitalWrite(PUMP_PIN, LOW);
      delay(500);
      digitalWrite(PUMP_PIN, HIGH);
    }

    delay(1000);
  }

  // Debouce para evitar conflito com relé
  // As alterações de estado do relé alteravam as leituras do sensor
  if (sensorValue != prevValue) {
    if ((millis() - prevTime) >= debounceDelay) {
      debouncedValue = sensorValue;
    }
    prevTime = millis();
    prevValue = sensorValue;
  }

  // Verifica se a umidade está abaixo do definido como minimo
  if (debouncedValue <= SENSOR_TRIGGER) {
    // Solo está seco
    digitalWrite(PUMP_PIN, LOW);
    digitalWrite(RED_LED_PIN, HIGH);
    digitalWrite(GREEN_LED_PIN, LOW);
    
    // Evita que o led vermelho fique piscando sem parar
    if ((millis() - blinkTime) >= blinkInterval) {
      blinkTime = millis();
      blinkState = !blinkState;
      digitalWrite(RED_LED_PIN, blinkState);
    }
  } else {
    // Solo está umido
    digitalWrite(PUMP_PIN, HIGH);
    digitalWrite(RED_LED_PIN, LOW);
    digitalWrite(GREEN_LED_PIN, HIGH);
  }
}
