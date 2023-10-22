CREATE TABLE post (
    id int PRIMARY KEY,
    rating int NOT NULL,
    score int NOT NULL,
    source varchar(500) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT now());

CREATE TABLE tag (
    id SERIAL PRIMARY KEY,
    name varchar(100) NOT NULL);

CREATE TABLE post_tag (
    id int PRIMARY KEY,
    post_id int NOT NULL,
    tag_id int NOT NULL,
    PRIMARY KEY (id, post_id, tag_id),
    FOREIGN KEY (post_id) REFERENCES post(id),
    FOREIGN KEY (tag_id) REFERENCES tag(id));

CREATE TABLE post_detail (
    post_id int PRIMARY KEY,
    preview_url varchar(500) NOT NULL,
    file_url varchar(500) NOT NULL,
    FOREIGN KEY (post_id) REFERENCES post(id));