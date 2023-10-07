use std::fs::File;
use std::io::{BufRead, BufReader};

struct Point2D {
    x: f64,
    y: f64,
}

fn read_points_from_file(filename: &str) -> Vec<Point2D> {
    let file = File::open(filename).expect("Failed to open file");
    let reader = BufReader::new(file);
    let mut points = Vec::new();
    let mut first_line = true;

    for line in reader.lines() {
        if first_line {
            first_line = false;
            continue;
        }
        let line = line.expect("Failed to read line");
        let values: Vec<f64> = line.split_whitespace()
                                   .map(|s| s.parse().unwrap())
                                   .collect();
        let point = (values[0], values[1]);
        points.push(point);
    }

    points
}

fn exhaustive(points: &[Point2D], route: &[usize], time_limit: Duration) -> Vec<usize> {
    let mut sw = Instant::now();
    let mut best = route.to_vec();
    let mut route_cost = utils::cycle_distance_squared(points, route);
    let mut best_cost = f64::INFINITY;
    let mut improved = true;
    let mut num_checks = 0;
    let mut last_print = Duration::from_secs(0);
    let mut time_limit_reached = false;

    while improved && !time_limit_reached {
        improved = false;
        for i in 1..points.len() - 2 {
            if time_limit_reached {
                break;
            }
            for j in i + 2..points.len() + 1 {
                let jj = j % points.len();
                if (jj as i32 - i as i32).abs() < 2 {
                    continue;
                }
                let jprev = if jj == 0 { points.len() - 1 } else { jj - 1 };
                let (a, b, c, d) = (route[i-1], route[i], route[jprev], route[jj]);
                let cost_removed =
                      points[a].distance_squared(&points[b])
                    + points[c].distance_squared(&points[d]);
                let cost_added =
                      points[a].distance_squared(&points[c])
                    + points[b].distance_squared(&points[d]);
                let new_cost = route_cost - cost_removed + cost_added;
                if new_cost < best_cost {
                    best.copy_from_slice(route);
                    utils::reverse_range(&mut best, i, j - 1);
                    best_cost = new_cost;
                    improved = true;
                }
                num_checks += 1;
                if sw.elapsed() - last_print > Duration::from_secs(1) {
                    eprintln!("best: {}     Checks: {}", best_cost, num_checks);
                    last_print = sw.elapsed();
                    num_checks = 0;
                }
                if sw.elapsed() > time_limit {
                    time_limit_reached = true;
                    break;
                }
            }
        }
        route.copy_from_slice(&best);
        route_cost = best_cost;
    }
    best
}

fn main() {
    println!("Hello, world!");
}