import time
import tracemalloc
from clock import Clock
def clock(seconds):
    clock = Clock()
    print(f"Clock started")
    for sec in range(seconds):
        clock.tick()
    print(f"End")
def main():
    tracemalloc.start()
    start_time = time.time()
    clock(10200000)
    end_time = time.time()
    elapsed_time = (end_time - start_time) * 1_000
    print(f"Time elapsed: {elapsed_time:,.0f} ms")
    current_memory, peak_memory = tracemalloc.get_traced_memory()
    tracemalloc.stop()

    print(f"Current physical memory usage: {current_memory} bytes")

    print(f"Peak physical memory usage: {peak_memory} bytes")


if __name__ == "__main__":
    main()