MatrixApp

MatrixApp to aplikacja konsolowa napisana w C#, która wykonuje operacje na macierzach, w tym mnożenie macierzy przy użyciu różnych technik wielowątkowości (równoległe oraz przy użyciu własnych wątków). Program oferuje również możliwość przeprowadzenia testu wydajnościowego (benchmarku) dla różnych rozmiarów macierzy i liczby wątków.

Spis treści
	•	Funkcjonalności
	•	Technologie
	•	Instalacja i uruchomienie
	•	Przykład użycia
	•	Opis działania
	•	Benchmark
	•	Model danych
	•	Uwaga dot. wielkości macierzy
	•	Licencja

Funkcjonalności
	•	Mnożenie macierzy z wykorzystaniem różnych metod wielowątkowych:
	•	Równoległe przetwarzanie (Parallel.For)
	•	Przetwarzanie przy pomocy własnych wątków (Thread)
	•	Możliwość uruchomienia benchmarku porównującego czas wykonania dla różnych liczby wątków oraz rozmiarów macierzy
	•	Zapis wyników benchmarku do pliku CSV
	•	Obsługuje dowolne rozmiary macierzy oraz liczbę wątków

Technologie
	•	C#
	•	.NET
	•	Wątki i równoległość (Thread, Parallel.For)
	•	Operacje na macierzach
	•	StreamWriter do zapisu wyników do pliku CSV

Instalacja i uruchomienie
	1.	Sklonuj repozytorium:

git clone https://github.com/twoj-login/matrixapp.git
cd matrixapp

	2.	Zainstaluj zależności (jeśli są wymagane):

Aplikacja nie wymaga zewnętrznych zależności, działa na standardowej bibliotece .NET.
	3.	Uruchom aplikację:

dotnet run -- <threads_num> <size> [benchmark]

Gdzie:
	•	<threads_num>: liczba wątków do użycia (np. 4)
	•	<size>: rozmiar macierzy (np. 500 dla 500x500)
	•	[benchmark]: opcjonalnie, jeśli chcesz uruchomić benchmark (porównanie czasów różnych metod)

Przykład użycia

Uruchomienie testu mnożenia macierzy:

dotnet run -- 4 500

Powyższe polecenie wykonuje mnożenie dwóch macierzy o rozmiarze 500x500 z użyciem 4 wątków.

Uruchomienie benchmarku:

dotnet run -- 4 500 benchmark

Powyższe polecenie uruchamia benchmark, który porównuje czasy wykonania dla różnych liczby wątków (1, 2, 4, 8, 16, 32) oraz różnych rozmiarów macierzy (100x100, 200x200, 400x400, 800x800).

Opis działania

Program wykonuje operację mnożenia dwóch macierzy, a wynik jest obliczany na dwa sposoby:
	1.	Mnożenie przy użyciu Parallel.For:
	•	Równoległe przetwarzanie — każda linia macierzy jest przetwarzana w oddzielnym wątku.
	•	Szybsze dla większych macierzy i większej liczby wątków.
	2.	Mnożenie przy użyciu własnych wątków (Thread):
	•	Każdy wątek obsługuje część macierzy (np. podział na wiersze).
	•	Również umożliwia wielowątkowość, ale jest mniej optymalny w porównaniu do metody Parallel.For.

Obie metody zostały zaimplementowane w klasie Matrix, która zawiera następujące operacje:
	•	GenerateRandom(int rows, int cols): Generuje losową macierz o podanym rozmiarze.
	•	MultiplyParallel(Matrix A, Matrix B, int maxThreads): Mnożenie macierzy przy użyciu Parallel.For.
	•	MultiplyThreaded(Matrix A, Matrix B, int threadCount): Mnożenie macierzy przy użyciu własnych wątków.

Benchmark

Aby uruchomić benchmark, należy użyć opcji benchmark podczas uruchamiania aplikacji. Program wykona mnożenie macierzy dla różnych rozmiarów i liczby wątków, a następnie zapisze wyniki w pliku CSV w formacie:

MatrixSize;Threads;AverageTimeMsParallel;AverageTimeMsThread
100x100;1;15;20
100x100;2;10;15
...

Wyniki są zapisywane w pliku benchmark_results.csv.

Przykładowe wyniki benchmarku

![Benchmark Results](tables.png)

Wykres porównujący czas wykonania w zależności od liczby wątków

![Benchmark Results](plot.png)



W zależności od liczby wątków i rozmiaru macierzy, wydajność programu może się różnić. Zwiększenie liczby wątków może poprawić wydajność przy większych macierzach, jednak dla małych macierzy wielowątkowość może nie przynieść dużych korzyści.



Autor: [Aleksander Łyskawa]