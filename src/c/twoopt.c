#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <time.h>

typedef struct {
    double x;
    double y;
} Point2D;

double distance_squared(Point2D* p1, Point2D* p2) {
    double xdiff = p1->x - p2->x;
    double ydiff = p1->y - p2->y;
    return xdiff * xdiff + ydiff * ydiff;
}

Point2D* read_points_from_file(char* filename, int* num_points) {
    FILE* file = fopen(filename, "r");
    if (file == NULL) {
        printf("Failed to open file\n");
        exit(1);
    }
    char buffer[1024];
    fgets(buffer, 1024, file); // skip first line
    int n = 0;
    while (fgets(buffer, 1024, file) != NULL) {
        n++;
    }
    rewind(file);
    fgets(buffer, 1024, file); // skip first line
    Point2D* points = (Point2D*) malloc(n * sizeof(Point2D));
    for (int i = 0; i < n; i++) {
        double x, y;
        fscanf(file, "%lf %lf", &x, &y);
        points[i].x = x;
        points[i].y = y;
    }
    fclose(file);
    *num_points = n;
    return points;
}

double cycle_distance_squared(Point2D* points, int* route, int num_points) {
    double distance = 0.0;
    for (int i = 0; i < num_points - 1; i++) {
        int a = route[i];
        int b = route[i + 1];
        distance += distance_squared(&points[a], &points[b]);
    }
    distance += distance_squared(&points[route[num_points - 1]], &points[route[0]]);
    return distance;
}

void reverse_range(int* array, int start, int end) {
    while (start < end) {
        int temp = array[start];
        array[start] = array[end];
        array[end] = temp;
        start++;
        end--;
    }
}

void exhaustive(Point2D* points, int num_points, int* route, int time_limit) {
    clock_t start_time = clock();
    int* best = (int*) malloc(num_points * sizeof(int));
    for (int i = 0; i < num_points; i++) {
        best[i] = route[i];
    }
    double route_cost = cycle_distance_squared(points, route, num_points);
    double best_cost = INFINITY;
    int improved = 1;
    int num_checks = 0;
    clock_t last_print = 0;
    int time_limit_reached = 0;

    while (improved && !time_limit_reached) {
        improved = 0;
        for (int i = 1; i < num_points - 2; i++) {
            if (time_limit_reached) {
                break;
            }
            for (int j = i + 2; j < num_points + 1; j++) {
                int jj = j % num_points;
                if (abs(jj - i) < 2) {
                    continue;
                }
                int jprev = jj == 0 ? num_points - 1 : jj - 1;
                int a = route[i-1], b = route[i], c = route[jprev], d = route[jj];
                double cost_removed = distance_squared(&points[a], &points[b]) + distance_squared(&points[c], &points[d]);
                double cost_added = distance_squared(&points[a], &points[c]) + distance_squared(&points[b], &points[d]);
                double new_cost = route_cost - cost_removed + cost_added;
                if (new_cost < best_cost) {
                    for (int k = 0; k < num_points; k++) {
                        best[k] = route[k];
                    }
                    reverse_range(best, i, j - 1);
                    best_cost = new_cost;
                    improved = 1;
                }
                num_checks++;
                if ((double) (clock() - last_print) / CLOCKS_PER_SEC > 1.0) {
                    printf("best: %lf     Checks: %d\n", best_cost, num_checks);
                    last_print = clock();
                    num_checks = 0;
                }
                if ((double) (clock() - start_time) / CLOCKS_PER_SEC > time_limit) {
                    time_limit_reached = 1;
                    break;
                }
            }
        }
        for (int i = 0; i < num_points; i++) {
            route[i] = best[i];
        }
        route_cost = best_cost;
    }
    free(best);
}

int main(int argc, char** argv) {
    if (argc < 2) {
        printf("Usage: %s <filename>\n", argv[0]);
        return 1;
    }
    char* filename = argv[1];
    printf("Reading points from %s\n", filename);

    int num_points;
    Point2D* points = read_points_from_file(filename, &num_points);
    int* route = (int*) malloc(num_points * sizeof(int));
    for (int i = 0; i < num_points; i++) {
        route[i] = i;
    }

    exhaustive(points, num_points, route, 4);

    free(points);
    free(route);
    return 0;
}
