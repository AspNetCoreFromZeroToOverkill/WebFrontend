``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9700K CPU 3.60GHz (Coffee Lake), 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=3.0.100-preview9-014004
  [Host]     : .NET Core 3.0.0-preview9-19423-09 (CoreCLR 4.700.19.42102, CoreFX 4.700.19.42104), 64bit RyuJIT
  DefaultJob : .NET Core 3.0.0-preview9-19423-09 (CoreCLR 4.700.19.42102, CoreFX 4.700.19.42104), 64bit RyuJIT


```
|                                                               Method | MaxRoutes |         Mean |       Error |     StdDev |  Ratio | RatioSD | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------------------------------------------------- |---------- |-------------:|------------:|-----------:|-------:|--------:|-----:|-------:|------:|------:|----------:|
|                            **Attempt01DictionaryPlusStringManipulation** |        **10** |     **40.84 ns** |   **0.1758 ns** |  **0.1644 ns** |   **1.00** |    **0.00** |    **1** | **0.0216** |     **-** |     **-** |     **136 B** |
|                            Attempt02ArrayIterationPlusPathBeginsWith |        10 |    165.82 ns |   1.3302 ns |  1.2443 ns |   4.06 |    0.04 |    5 |      - |     - |     - |         - |
|           Attempt03HashCodeBasedDoubleDictionaryPlusSpanManipulation |        10 |     87.56 ns |   0.4744 ns |  0.4438 ns |   2.14 |    0.02 |    4 |      - |     - |     - |         - |
| Attempt04HashCodeBasedDictionaryWithComplexValuePlusSpanManipulation |        10 |     74.57 ns |   0.6426 ns |  0.6011 ns |   1.83 |    0.01 |    3 |      - |     - |     - |         - |
|                                    Attempt0504WithAggressiveInlining |        10 |     71.48 ns |   1.4713 ns |  1.3762 ns |   1.75 |    0.03 |    2 |      - |     - |     - |         - |
|                                                                      |           |              |             |            |        |         |      |        |       |       |           |
|                            **Attempt01DictionaryPlusStringManipulation** |       **100** |     **44.05 ns** |   **0.6808 ns** |  **0.6368 ns** |   **1.00** |    **0.00** |    **1** | **0.0216** |     **-** |     **-** |     **136 B** |
|                            Attempt02ArrayIterationPlusPathBeginsWith |       100 |  1,690.55 ns |   3.2448 ns |  3.0352 ns |  38.39 |    0.56 |    5 |      - |     - |     - |         - |
|           Attempt03HashCodeBasedDoubleDictionaryPlusSpanManipulation |       100 |     90.44 ns |   0.6025 ns |  0.5636 ns |   2.05 |    0.03 |    4 |      - |     - |     - |         - |
| Attempt04HashCodeBasedDictionaryWithComplexValuePlusSpanManipulation |       100 |     74.07 ns |   0.3004 ns |  0.2810 ns |   1.68 |    0.03 |    3 |      - |     - |     - |         - |
|                                    Attempt0504WithAggressiveInlining |       100 |     72.27 ns |   0.4487 ns |  0.3977 ns |   1.64 |    0.03 |    2 |      - |     - |     - |         - |
|                                                                      |           |              |             |            |        |         |      |        |       |       |           |
|                            **Attempt01DictionaryPlusStringManipulation** |      **1000** |     **41.60 ns** |   **0.1311 ns** |  **0.1162 ns** |   **1.00** |    **0.00** |    **1** | **0.0216** |     **-** |     **-** |     **136 B** |
|                            Attempt02ArrayIterationPlusPathBeginsWith |      1000 | 17,640.81 ns | 103.7063 ns | 97.0069 ns | 423.86 |    2.53 |    5 |      - |     - |     - |         - |
|           Attempt03HashCodeBasedDoubleDictionaryPlusSpanManipulation |      1000 |     92.85 ns |   0.1979 ns |  0.1851 ns |   2.23 |    0.01 |    4 |      - |     - |     - |         - |
| Attempt04HashCodeBasedDictionaryWithComplexValuePlusSpanManipulation |      1000 |     80.62 ns |   1.1987 ns |  1.1213 ns |   1.94 |    0.02 |    3 |      - |     - |     - |         - |
|                                    Attempt0504WithAggressiveInlining |      1000 |     77.01 ns |   0.0731 ns |  0.0610 ns |   1.85 |    0.01 |    2 |      - |     - |     - |         - |
